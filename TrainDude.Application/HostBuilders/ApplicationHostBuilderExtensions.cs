// <copyright file="ApplicationHostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.HostBuilders;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Application.Services;
using TrainDude.Application.Validation;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class ApplicationHostBuilderExtensions
{
    /// <summary>
    /// Adds to the collection service descriptors services required by the Network component.
    /// </summary>
    /// <param name="services">Collection of service descriptors.</param>
    /// <returns>Collection of service descriptors with services added.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddSingleton<SeedService>();
    }

    public static IServiceCollection AddRequests(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationHostBuilderExtensions).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }

    public static IServiceCollection AddDataValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(GetRadiiQuery).Assembly);

        return services;
    }
}