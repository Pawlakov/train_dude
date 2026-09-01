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

using TrainDude.Commands.Endpoints.Lines;
using TrainDude.Commands.Endpoints.Segments;
using TrainDude.Commands.Endpoints.Settings;
using TrainDude.Commands.Endpoints.Stations;
using TrainDude.Features.Lines.Projections;
using TrainDude.Features.Network.GetNetwork;
using TrainDude.Features.Radii.Projections;
using TrainDude.Features.Stations.Projections;
using TrainDude.Features.Trips.Projections;
using TrainDude.Features.Network.Network;
using TrainDude.Features.Network.Projections;
using TrainDude.Features.Segments.Projections;
using TrainDude.Features.Shared.Projections;
using TrainDude.Web.ExceptionHandlers;

using Wolverine.Http;
using Wolverine.Marten;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddRequestHandlers(this IServiceCollection services)
    {
        services
            .AddMediator(options =>
            {
                options.Assemblies =
                [
                    typeof(GetNetworkQuery),
                    typeof(GetNetworkQueryHandler),
                ];
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

        return services;
    }

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

                options.Projections.Add<SettingsReferenceReadModelProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<LineAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineReadModelProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineSegmentReferenceProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<LineTripReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<RadiusProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<SegmentAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<SegmentReadModelProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<SegmentStationReferenceProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<StationAggregateProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<StationReadModelProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<TripProjection>(ProjectionLifecycle.Inline);

                options.Projections.Add<NetworkReadModelProjection>(ProjectionLifecycle.Inline);

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