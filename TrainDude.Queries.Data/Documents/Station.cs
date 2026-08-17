// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;

using LiteDB;

using TrainDude.Integration.Values;

public class Station
{
    [BsonId]
    public Guid StationId { get; set; }

    public long Version { get; set; }

    public string Name { get; set; }

    public Location? Location { get; set; }
}