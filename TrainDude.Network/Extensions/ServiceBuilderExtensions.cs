// <copyright file="ServiceBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TrainDude.Network.Models;
using TrainDude.Network.Services;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class ServiceBuilderExtensions
{
    /// <summary>
    /// Adds to the collection service descriptors services required by the Network component.
    /// </summary>
    /// <param name="services">Collection of service descriptors.</param>
    /// <returns>Collection of service descriptors with services added.</returns>
    public static IServiceCollection AddNetworkServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<SeedService>()
            .AddSingleton<StationService>()
            .AddSingleton<RouteService>()
            .AddSingleton<IMongoCollection<Station>>(services => services.GetRequiredService<IMongoDatabase>().GetCollection<Station>("stations"))
            .AddSingleton<IMongoCollection<Route>>(services => services.GetRequiredService<IMongoDatabase>().GetCollection<Route>("routes"));
    }
}