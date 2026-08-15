// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.HostBuilders;

using FluentValidation;

using JasperFx;
using JasperFx.Events.Projections;

using Marten;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.Admin;
using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Domain.Documents;

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

                options.Projections.Snapshot<Line>(SnapshotLifecycle.Inline);
                options.Projections.Snapshot<Radius>(SnapshotLifecycle.Inline);
                options.Projections.Snapshot<Segment>(SnapshotLifecycle.Inline);
                options.Projections.Snapshot<Settings>(SnapshotLifecycle.Inline);
                options.Projections.Snapshot<Station>(SnapshotLifecycle.Inline);
                options.Projections.Snapshot<Trip>(SnapshotLifecycle.Inline);

                if (isDevelopment)
                {
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                }
            })
            .UseLightweightSessions();

        return services;
    }

    public static IServiceCollection AddWriteDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(SeedCommand).Assembly)
            .AddValidatorsFromAssembly(typeof(SeedCommandHandler).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(WriteValidationBehavior<,>));

        return services;
    }
}