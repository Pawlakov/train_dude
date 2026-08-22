// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Queries.Contracts.Network;
using TrainDude.Queries.Handlers.Network;
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
}