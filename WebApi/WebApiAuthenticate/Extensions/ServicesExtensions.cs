using EntityFramework;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PasswordHasher;
using Repositories.Abstractions;
using Repositories.Implementations.EntityFrameworkRepositories;
using Services.Abstractions;
using Services.Implementations;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Enums;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TokenProvider;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiAuthenticate.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection InstallRepositories(this IServiceCollection services)
        => services.AddScoped<IUserRepository, UserRepository>();

    public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
        => services.AddTransient<IUserManagementService, UserManagementService>()
                .AddTransient<INotificationService, NotificationService>()
                .AddTransient<IUserValidationService, UserValidationService>();

    public static IServiceCollection InstallInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var userDbConString = configuration.GetConnectionString("UsersDb");
        if (string.IsNullOrWhiteSpace(userDbConString))
            throw new InvalidOperationException("The connection string 'UsersDb' cannot be null or empty.");


        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.ValidationStrategy = ValidationStrategy.All;
        }).AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDbContext<UserDbContext>(options => options.UseNpgsql(userDbConString,
                opt => opt.MigrationsAssembly("EntityFramework")))
            .AddTransient<IPasswordHasher, CustomPasswordHasher>()
            .AddTransient<IJwtProvider, JwtProvider>()
            .Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        return services;
    }
}