// <copyright file="Radius.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using LiteDB;

public class Radius
{
    [BsonId]
    public int RadiusId { get; set; }

    public int Speed { get; set; }

    public int Minimum { get; set; }
}