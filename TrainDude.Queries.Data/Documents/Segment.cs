// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System.Collections.Generic;

using LiteDB;

using TrainDude.Shared.Values;

public class Segment
{
    [BsonId]
    public int SegmentId { get; set; }

    public double? NominalLength { get; set; }

    public int AStationId { get; set; }

    public string AName { get; set; }

    public Location? ALocation { get; set; }

    public int BStationId { get; set; }

    public string BName { get; set; }

    public Location? BLocation { get; set; }

    public ICollection<Location> Vertices { get; set; }
}