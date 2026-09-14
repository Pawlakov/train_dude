// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.HostBuilders;

using System;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Settings.Domain;
using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Trips.Domain;
using TrainDude.Infrastructure.Lines.Projections;
using TrainDude.Infrastructure.Segments.Projections;
using TrainDude.Infrastructure.Shared.Projections;
using TrainDude.Infrastructure.Stations.Projections;

using Wolverine.Marten;
using Wolverine.Marten;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddEventStore(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.DatabaseSchemaName = "train_dude";

                options.Projections.Snapshot<SettingsAggregate>(SnapshotLifecycle.Inline);
                options.Projections.Add<SharedSettingsReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Snapshot<LineAggregate>(SnapshotLifecycle.Inline);
                options.Projections.Add<LineReadModelProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<LineStationReferenceProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineSegmentReferenceProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineTripReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Snapshot<RadiusAggregate>(SnapshotLifecycle.Inline);

                options.Projections.Snapshot<SegmentAggregate>(SnapshotLifecycle.Inline);
                options.Projections.Add<SegmentReadModelProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<SegmentStationReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Snapshot<StationAggregate>(SnapshotLifecycle.Inline);
                options.Projections.Add<StationReadModelProjection>(ProjectionLifecycle.Async);

                options.Projections.Snapshot<TripAggregate>(SnapshotLifecycle.Inline);

                if (isDevelopment)
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            })
            .UseLightweightSessions()
            .IntegrateWithWolverine()
            .AddAsyncDaemon(DaemonMode.Solo);

        return services;
    }
}