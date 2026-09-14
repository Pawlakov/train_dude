// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Web.ExceptionHandlers;

using Wolverine.Http;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    public static IServiceCollection AddHttpHandlers(this IServiceCollection services)
    {
        services.AddWolverineHttp();

        services.AddExceptionHandler<DomainExceptionHandler>();
        services.AddExceptionHandler<ConcurrencyExceptionHandler>();

        return services;
    }
}