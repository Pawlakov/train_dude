// <copyright file="Trip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Aggregates;

using System;

using LiteDB;

public class Trip
{
    [BsonId]
    public Guid TripId { get; set; }

    public int TripNumber { get; set; }
}