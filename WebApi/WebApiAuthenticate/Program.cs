using EntityFramework;
using FluentValidation;
using FluentValidation.AspNetCore;
using PasswordHasher;
using Repositories.Abstractions;
using Repositories.Implementations.EntityFrameworkRepositories;
using Services.Abstractions;
using Services.Implementations;
using Services.Implementations.Mapping;
using WebApiAuthenticate.Mapping;
using WebApiAuthenticate.ModelsValidators;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var userDbConString = configuration.GetConnectionString("UsersDb");

// Add DbContext to the container.
services.ConfigureContext(userDbConString);

//Add repositories to the container.
services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
services.AddTransient<IUserManagementService, UserManagementService>();

// Add infrastructure to the container.
services.AddTransient<IPasswordHasher, CustomPasswordHasher>();
services.AddAutoMapper(
    cfg =>
    {
        cfg.AddProfile<UserMappingsPresentationProfile>();
        cfg.AddProfile(new GuestMappingsProfile(services.BuildServiceProvider().GetService<IPasswordHasher>()));
        cfg.AddProfile<UserMappingsApplicationProfile>();
    });
services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<ChangeEmailValidator>()
    .AddValidatorsFromAssemblyContaining<ChangePasswordValidator>()
    .AddValidatorsFromAssemblyContaining<ChangeUsernameValidator>()
    .AddValidatorsFromAssemblyContaining<CreatingUserValidator>()
    .AddValidatorsFromAssemblyContaining<VerifyEmailValidator>();
//оформить Extension методом

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();