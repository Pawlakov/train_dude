// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.HostBuilders;

using System;

using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;

using Marten;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Endpoints.Admin;
using TrainDude.Commands.Endpoints.Infrastructure;
using TrainDude.Commands.Endpoints.Lines;
using TrainDude.Commands.Endpoints.Radii;
using TrainDude.Commands.Endpoints.Segments;
using TrainDude.Commands.Endpoints.Settings;
using TrainDude.Commands.Endpoints.Stations;
using TrainDude.Commands.Endpoints.Trips;
using TrainDude.Domain.Base;

using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Marten;

/*
Does this require domain state?
    no -> FluentValidation
        quantity > 0
        email has valid format
        currency is supplied
        ...
Is this rule fundamentally about executing this particular use case?
    yes -> Handler
        customer exists
        customer is active
        external credit check passed
        coupon exists
        user is allowed to perform this particular operation
        ...
Is this an invariant of the aggregate or its state transition?
    yes -> Aggregate
        shipped orders cannot be cancelled
        a bank account cannot have an invalid balance
        a reservation cannot be confirmed twice
        a completed workflow cannot transition back to Draft
        ...
*/

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteServices(this IServiceCollection services, string connectionString, bool isDevelopment)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

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

        services.AddWolverineHttp();

        services.AddExceptionHandler<DomainExceptionHandler>();
        services.AddExceptionHandler<ConcurrencyExceptionHandler>();

        return services;
    }

    public static IHostBuilder UseWriteServices(this IHostBuilder host)
    {
        host.UseWolverine(opts =>
        {
            opts.Policies.AutoApplyTransactions();
            opts.Policies.UseDurableLocalQueues();
            opts.Policies.OnException<DomainException>().MoveToErrorQueue();

            opts.UseFluentValidation();

            // TODO some day we will do it this way
            // opts.PublishMessage<TripCreatedIntegrationEvent>().ToRabbitQueue("train-dude-projection").UseDurableInbox();
        });

        return host;
    }
}