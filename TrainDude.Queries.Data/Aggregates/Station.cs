// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Aggregates;

using LiteDB;

using TrainDude.Shared.Values;

public class Station
{
    [BsonId]
    public int StationId { get; set; }

    public string? NameGerman { get; set; }

    public string? NameGermanNew { get; set; }

    public Location? Location { get; set; }
}