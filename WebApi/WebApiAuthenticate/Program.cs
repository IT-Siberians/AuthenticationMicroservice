using EntityFramework;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PasswordHasher;
using Repositories.Abstractions;
using Repositories.Implementations.EntityFrameworkRepositories;
using Services.Abstractions;
using Services.Implementations;
using System.Text;
using TokenProvider;
using WebApiAuthenticate.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var userDbConString = configuration.GetConnectionString("UsersDb");
if (string.IsNullOrWhiteSpace(userDbConString))
    throw new InvalidOperationException("The connection string 'UsersDb' cannot be null or empty.");

// Configure services.
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

// Add DbContext to the container.
services.AddDbContext<UserDbContext>(options => options.UseNpgsql(userDbConString,
    opt => opt.MigrationsAssembly("EntityFramework")));

// Add repositories to the container.
services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
services.AddTransient<IUserManagementService, UserManagementService>();
services.AddTransient<INotificationService, NotificationService>();
services.AddTransient<IUserValidationService, UserValidationService>();
services.AddTransient<ITokenService, TokenService>();

// Add infrastructure to the container.
services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
services.AddTransient<IPasswordHasher, CustomPasswordHasher>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived =
                context =>
                {
                    context.Token = context.Request.Cookies[jwtOptions.CookieName];

                    return Task.CompletedTask;
                }
        };
    });

services.AddControllers();

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
    //    c.RoutePrefix = string.Empty; // Доступ к Swagger UI по корневому URL
    //});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<UserDbContext>();

app.Run();