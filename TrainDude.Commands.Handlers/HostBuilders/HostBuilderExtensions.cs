// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.HostBuilders;

using FluentValidation;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.Admin;
using TrainDude.Commands.Handlers.Projections;
using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.Admin;

using Wolverine.Marten;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteDataServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
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

    public static IServiceCollection AddWriteDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(UpdateStationNameModeCommand).Assembly)
            .AddValidatorsFromAssembly(typeof(UpdateStationNameModeCommandHandler).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(WriteValidationBehavior<,>));

        return services;
    }
}