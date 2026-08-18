// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;
using System.Collections.Generic;

using LiteDB;

using TrainDude.Integration.Values;

public class Segment
{
    [BsonId]
    public Guid SegmentId { get; set; }

    public long Version { get; set; }

    public double? NominalLength { get; set; }

    public int AStationId { get; set; }

    public string AName { get; set; }

    public Location? ALocation { get; set; }

    public int BStationId { get; set; }

    public string BName { get; set; }

    public Location? BLocation { get; set; }

    public ICollection<Location> Vertices { get; set; }
}