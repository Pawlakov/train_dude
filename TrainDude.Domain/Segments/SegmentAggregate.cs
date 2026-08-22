// <copyright file="SegmentAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Segments;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;

public class SegmentAggregate
    : BaseAggregate
{
    [JsonConstructor]
    private SegmentAggregate(Guid id, long version, double? nominalLength, Guid aId, Guid bId)
    {
        this.Id = id;
        this.Version = version;

        this.NominalLength = nominalLength;
        this.AId = aId;
        this.BId = bId;
    }

    public SegmentAggregate()
    {
    }

    public double? NominalLength { get; private set; }

    public Guid AId { get; private set; }

    public Guid BId { get; private set; }

    public static SegmentCreated Make(Guid id, double? nominalLength, Guid aId, Guid bId)
    {
        return new SegmentCreated(id, DateTime.UtcNow, nominalLength, aId, bId);
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