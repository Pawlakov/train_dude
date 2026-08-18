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
    private Segment(Guid id, long version, double? nominalLength, Guid aId, Guid bId)
    {
        this.Id = id;
        this.Version = version;

        this.NominalLength = nominalLength;
        this.AId = aId;
        this.BId = bId;
    }

    public Segment()
    {
    }

    public double? NominalLength { get; private set; }

    public Guid AId { get; private set; }

    public Guid BId { get; private set; }

    public static SegmentCreated Make(Guid id, double? nominalLength, Guid aId, Guid bId)
    {
        return new SegmentCreated(id, nominalLength, aId, bId);
    }

    public void Apply(SegmentCreated e)
    {
        this.Id = e.Id;
        this.NominalLength = e.NominalLength;
        this.AId = e.AId;
        this.BId = e.BId;

        this.Version++;
    }
}