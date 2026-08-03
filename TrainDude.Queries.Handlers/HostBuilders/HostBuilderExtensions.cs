// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.HostBuilders;

using FluentValidation;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.GetNetworkGeoJsonQuery;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(GetNetworkQuery).Assembly)
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(ReadValidationBehavior<,>));

        return services;
    }
}