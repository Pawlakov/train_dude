// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.HostBuilders;

using JasperFx;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Data.Documents;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.DatabaseSchemaName = "train_dude";
                options.Projections.Snapshot<Station>(SnapshotLifecycle.Inline);
                if (isDevelopment)
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            })
            .UseLightweightSessions();

        return services;
    }
}