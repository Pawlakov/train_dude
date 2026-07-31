// <copyright file="ApplicationHostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.HostBuilders;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Application.Requests.GetRadiiQuery;
using TrainDude.Application.Validation;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class ApplicationHostBuilderExtensions
{
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