// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.HostBuilders;

using FluentValidation;

using LiteDB;

using Mediator;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Queries.Data.Documents;
using TrainDude.Queries.Handlers.Network;
using TrainDude.Queries.Handlers.Validation;
using TrainDude.Queries.Requests.Network;

/// <summary>
/// A container for extensions methods concerning services.
/// </summary>
public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadDataValidation(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(GetNetworkQuery).Assembly)
            .AddValidatorsFromAssembly(typeof(GetNetworkQueryHandler).Assembly)
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ReadValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddReadDataServices(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<ILiteDatabase>(new LiteDatabase(connectionString));

        services.AddSingleton<ILiteCollection<Line>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Line>("lines"));
        services.AddSingleton<ILiteCollection<Radius>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Radius>("radii"));
        services.AddSingleton<ILiteCollection<Segment>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Segment>("segments"));
        services.AddSingleton<ILiteCollection<Station>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Station>("stations"));
        services.AddSingleton<ILiteCollection<Trip>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Trip>("trips"));
        services.AddSingleton<ILiteCollection<Settings>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Settings>("settings"));

        return services;
    }
}