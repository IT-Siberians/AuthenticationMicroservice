using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using PasswordHasher;
using Repositories.Abstractions;
using Repositories.Implementations.InMemoryRepository;
using Services.Abstractions;
using Services.Implementations;
using Services.Implementations.Mapping;
using UserMappingsProfile = WebApiAuthenticate.Mapping.UserMappingsProfile;

namespace WebApiAuthenticate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        var services = builder.Services;
        
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddTransient<IMessageBusPublisher, MessageBusPublisher>();
        services.AddTransient<IUserManagementService, UserManagementService>();

        services.AddTransient<IPasswordHasher, CustomPasswordHasher>();
        services.AddAutoMapper(
            cfg =>
            {
                cfg.AddProfile<UserMappingsProfile>();
                cfg.AddProfile(new GuestMappingsProfile(services.BuildServiceProvider().GetService<IPasswordHasher>()));
                cfg.AddProfile<Services.Implementations.Mapping.UserMappingsProfile>();
            });

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
    }
    private static MapperConfiguration GetMapperConfiguration(IPasswordHasher hasher)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingsProfile>();
            cfg.AddProfile(new GuestMappingsProfile(hasher));
            cfg.AddProfile<Services.Implementations.Mapping.UserMappingsProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }
}

