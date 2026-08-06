// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.HostBuilders;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseSqlServer(connectionString);

            if (isDevelopment)
            {
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            }
        });

        services.AddScoped<IWriteDbContext, WriteDbContext>();

        return services;
    }
}