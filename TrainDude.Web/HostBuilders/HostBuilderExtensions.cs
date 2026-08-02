// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Application.GetNetworkGeoJsonQuery;
using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

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
                    typeof(GetNetworkGeoDataQuery),
                    typeof(GetNetworkGeoDataQueryHandler),
                ];
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

        return services;
    }
}