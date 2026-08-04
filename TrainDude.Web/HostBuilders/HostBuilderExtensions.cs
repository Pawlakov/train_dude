// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.DropAndSeedCommand;
using TrainDude.Commands.Requests.DropAndSeedCommand;
using TrainDude.Queries.Handlers.GetNetworkQuery;
using TrainDude.Queries.Requests.GetNetworkQuery;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services
            .AddMediator(options =>
            {
                options.Assemblies =
                [
                    typeof(GetNetworkQuery),
                    typeof(GetNetworkQueryHandler),
                    typeof(DropAndSeedCommand),
                    typeof(DropAndSeedCommandHandler),
                ];
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

        return services;
    }
}