// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.HostBuilders;

using FluentValidation;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Commands.Handlers.SeedCommand;
using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.SeedCommand;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddWriteDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(SeedCommand).Assembly)
            .AddValidatorsFromAssembly(typeof(SeedCommandHandler).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(WriteValidationBehavior<,>));

        return services;
    }
}