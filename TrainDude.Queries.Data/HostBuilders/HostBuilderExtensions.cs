// <copyright file="HostBuilderExtensions.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.HostBuilders;

using LiteDB;

using Microsoft.Extensions.DependencyInjection;

using TrainDude.Queries.Data.Aggregates;

public static class HostBuilderExtensions
{
    public static IServiceCollection AddReadDataServices(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<ILiteDatabase>(new LiteDatabase(connectionString));

        services.AddSingleton<ILiteCollection<Line>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Line>("lines"));
        services.AddSingleton<ILiteCollection<Radius>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Radius>("radii"));
        services.AddSingleton<ILiteCollection<Segment>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Segment>("segments"));
        services.AddSingleton<ILiteCollection<Station>>(x => x.GetRequiredService<ILiteDatabase>().GetCollection<Station>("stations"));

        return services;
    }
}