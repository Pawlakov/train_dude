// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Documents;

using System;
using System.Collections.Generic;

using LiteDB;

using TrainDude.Integration.Values;

public class Segment
    : IVersionedDocument
{
    [BsonId]
    public Guid Id { get; set; }

    public long Version { get; set; }

    public double? NominalLength { get; set; }

    public SegmentStation A { get; set; }

    public SegmentStation B { get; set; }

    public ICollection<Location> Vertices { get; set; }

    public class SegmentStation
    {
        public Guid StationId { get; set; }

        public string Name { get; set; }

        public Location? Location { get; set; }
    }
}