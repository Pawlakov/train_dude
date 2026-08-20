// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.HostBuilders;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.Projections;
using TrainDude.Commands.Handlers.Services;

using Wolverine.Marten;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        services.AddScoped<SettingsService>();
        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.DatabaseSchemaName = "train_dude";

                options.Projections.Add<LineProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<RadiusProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<SegmentProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<SettingsProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<StationProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<TripProjection>(ProjectionLifecycle.Inline);

                if (isDevelopment)
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            })
            .UseLightweightSessions()
            .IntegrateWithWolverine()
            .AddAsyncDaemon(DaemonMode.HotCold);

        return services;
    }
}