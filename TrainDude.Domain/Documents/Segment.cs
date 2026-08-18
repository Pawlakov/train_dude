// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Segments;

public class Segment
    : AggregateBase
{
    [JsonConstructor]
    private Segment(Guid id, long version, double? nominalLength)
    {
        this.Id = id;
        this.Version = version;

        this.NominalLength = nominalLength;
    }

    public Segment()
    {
    }

    public double? NominalLength { get; private set; }

    public static SegmentCreated Make(Guid id, double? nominalLength)
    {
        return new SegmentCreated(id, nominalLength);
    }

    public void Apply(SegmentCreated e)
    {
        this.Id = e.Id;
        this.NominalLength = e.NominalLength;

        this.Version++;
    }
}