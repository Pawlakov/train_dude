// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.SeedCommand;
using TrainDude.Commands.Requests.SeedCommand;
using TrainDude.Projections;
using TrainDude.Queries.Handlers.GetNetworkQuery;
using TrainDude.Queries.Requests.GetNetworkQuery;
using TrainDude.Shared.Notifications;
using TrainDude.Web.Infrastructure;

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
                    typeof(SeedCommand),
                    typeof(SeedCommandHandler),
                    typeof(DataChangedNotification),
                    typeof(DataChangedNotificationHandler),
                ];
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

        return services;
    }

    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}