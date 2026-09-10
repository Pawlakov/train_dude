// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using System;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Infrastructure.Lines.Projections;
using TrainDude.Infrastructure.Radii.Projections;
using TrainDude.Infrastructure.Segments.Projections;
using TrainDude.Infrastructure.Settings.Projections;
using TrainDude.Infrastructure.Stations.Projections;
using TrainDude.Infrastructure.Trips.Projections;
using TrainDude.Web.ExceptionHandlers;

using Wolverine.Http;
using Wolverine.Marten;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddWriteServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        services.AddMarten(options =>
            {
                options.Connection(connectionString);
                options.DatabaseSchemaName = "train_dude";

                options.Projections.Add<SharedSettingsReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<LineAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineReadModelProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<LineSegmentReferenceProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<LineTripReferenceProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<LineTripLinkProjection>(ProjectionLifecycle.Async);

                options.Projections.Add<RadiusAggregateProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<SegmentAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<SegmentReadModelProjection>(ProjectionLifecycle.Async);
                options.Projections.Add<SegmentStationReferenceProjection>(ProjectionLifecycle.Async);

                options.Projections.Add<StationAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<StationReadModelProjection>(ProjectionLifecycle.Async);

                options.Projections.Add<TripAggregateProjection>(ProjectionLifecycle.Inline);

                if (isDevelopment)
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            })
            .UseLightweightSessions()
            .IntegrateWithWolverine()
            .AddAsyncDaemon(DaemonMode.HotCold);

        services.AddWolverineHttp();

        services.AddExceptionHandler<DomainExceptionHandler>();
        services.AddExceptionHandler<ConcurrencyExceptionHandler>();

        return services;
    }
}