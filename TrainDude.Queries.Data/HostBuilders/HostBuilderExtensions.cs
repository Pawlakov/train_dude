// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.HostBuilders;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        services.AddDbContext<ReadDbContext>(options =>
        {
            options.UseSqlite(connectionString);

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        services.AddScoped<IRadiusRepository, ReadDbContext>();
        services.AddScoped<ISegmentRepository, ReadDbContext>();
        services.AddScoped<IStationRepository, ReadDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
}