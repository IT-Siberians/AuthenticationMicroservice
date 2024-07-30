﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFramework;

public static class EntityFrameworkInstaller
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UserDbContext>(
            opt => opt
                .UseInMemoryDatabase("inmem"));
        //.UseNpgsql(connectionString));
        return services;
    }
}