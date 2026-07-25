// <copyright file="ServiceBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Extensions;

using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using TrainDude.Data.Models;

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
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var path = Path.Join(folder, "TrainDude.db");

        return services
            .AddDbContext<NetworkDbContext>(options => options.UseSqlite($"Data Source={path}"));
    }
}