// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.HostBuilders;

using FluentValidation;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Application.Validation;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(GetNetworkGeoJsonQuery).Assembly)
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}