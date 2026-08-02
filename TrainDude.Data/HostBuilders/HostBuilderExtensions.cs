// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.HostBuilders;

using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        services.AddDbContext<NetworkDbContext>(options =>
        {
            options.UseSqlite(connectionString);

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        services.AddScoped<INetworkDbContext, NetworkDbContext>();

        return services;
    }
}