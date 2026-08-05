// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections.HostBuilders;

using Microsoft.Extensions.DependencyInjection;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddDataModelProjections(this IServiceCollection services)
    {
        services.AddScoped<DataModelProjector>();

        return services;
    }
}