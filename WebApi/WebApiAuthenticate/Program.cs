using AuthenticationDataManager.Cookies;
using EntityFramework;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MessageBusClient;
using MessageBusClient.Consumers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Otus.QueueDto.User;
using PasswordHasher;
using Redis;
using Repositories.Abstractions;
using Repositories.Implementations.EntityFrameworkRepositories;
using Repositories.Implementations.RedisRepositories;
using Services.Abstractions;
using Services.Implementations;
using StackExchange.Redis;
using WebApiAuthenticate.Extensions;
using WebApiAuthenticate.Helpers;
using WebApiAuthenticate.Middlewares.AuthorizationMiddlewares;
using WebApiAuthenticate.Middlewares.AuthorizationMiddlewares.AuthorizePolitics.IsOwner;
using static WebApiAuthenticate.Helpers.CustomAuthorizationMessages;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var userDbConString = configuration.GetConnectionString("UsersDb");
if (string.IsNullOrWhiteSpace(userDbConString))
    throw new InvalidOperationException("The connection string 'UsersDb' cannot be null or empty.");
var rmqConString = configuration.GetConnectionString(nameof(MassTransitProducer));
if (string.IsNullOrWhiteSpace(rmqConString))
    throw new InvalidOperationException($"The connection string '{nameof(MassTransitProducer)}' cannot be null or empty.");
var redisConString = configuration.GetConnectionString(nameof(RedisContext));
if (string.IsNullOrWhiteSpace(rmqConString))
    throw new InvalidOperationException($"The connection string '{nameof(RedisContext)}' cannot be null or empty.");


// Configure services
services.Configure<VerificationCodeRepositoryOptions>(configuration.GetSection(nameof(VerificationCodeRepositoryOptions)));

// Add DbContext to the container.
services.AddDbContext<UserDbContext>(options => options.UseNpgsql(userDbConString,
    opt => opt.MigrationsAssembly("EntityFramework")));
var redis = ConnectionMultiplexer.Connect(redisConString);
services.AddSingleton<IConnectionMultiplexer>(redis);
services.AddScoped<RedisContext>();

// Add repositories to the container.
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

// Add services to the container.
services.AddTransient<IUserManagementService, UserManagementService>();
services.AddTransient<INotificationService, NotificationService>();
services.AddTransient<IUserValidationService, UserValidationService>();
services.AddTransient<IMessageBusProducer, MassTransitProducer>();
services.AddTransient<IVerificationCodeService, VerificationCodeService>();
services.AddScoped<IAuthManagerService, CookiesManagerService>();
services.AddTransient<IClaimsPrincipalBuilder<CookiesClaimsPrincipalBuilder>, CookiesClaimsPrincipalBuilder>();
services.AddTransient<INotificationEventFactory, UserNotificationEventFactory>();
services.AddSingleton<IAuthorizationHandler, IsOwnerHandler>();


// Add infrastructure to the container.
services.AddTransient<IPasswordHasher, CustomPasswordHasher>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

services.AddAuthorization(options =>
{
    options.InvokeHandlersAfterFailure = false;
    options.AddPolicy(AttributePoliticsNameHelpers.OWNER_ONLY_POLITIC_NAME, policy =>
        policy.Requirements.Add(new IsOwnerRequirement()));
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
        options =>
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    if (context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_REASON_ITEM_KEY] != null)
                        return Task.CompletedTask;
                    context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_REASON_ITEM_KEY] = STANDART_USER_NOT_AUTHENTICATION_ERROR_MESSAGE;
                    context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_STATUS_CODE_ITEM_KEY] = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    if (context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_REASON_ITEM_KEY] != null)
                        return Task.CompletedTask;
                    context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_REASON_ITEM_KEY] = STANDART_ACCESS_DENIED_ERROR_MESSAGE;
                    context.HttpContext.Items[CustomAuthorizationConstants.FAILURE_STATUS_CODE_ITEM_KEY] = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            });

services.AddMassTransit(x =>
{
    x.AddConsumer<MassTransitConsumer<UpdateUserEvent>>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rmqConString);
        cfg.ReceiveEndpoint(
            $"{nameof(UpdateUserEvent)}.Auth",
            e =>
            {
                e.ConfigureConsumer<MassTransitConsumer<UpdateUserEvent>>(context);
            });

        cfg.ConfigureEndpoints(context);

    });
});

// Add consumer services.
services.AddScoped<IMessageProcessService<UpdateUserEvent>, UpdateUserProcessService>();

services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(
c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Authenticate API",
        Description = "Authentication and registration API used for authentication, user creation and management (registration, password change, username change, email change, user deletion)"
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    //    c.RoutePrefix = string.Empty; // Äîñòóï ê Swagger UI ïî êîðíåâîìó URL
    //});
}

app.UseCors(policy =>
{
    policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<UserDbContext>();

app.Run();