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
    private Segment(Guid id, long version)
    {
        this.Id = id;
        this.Version = version;
    }

    public Segment()
    {
    }

    public static SegmentCreated Make(Guid id)
    {
        return new SegmentCreated(id);
    }

    public void Apply(SegmentCreated e)
    {
        this.Id = e.Id;

        this.Version++;
    }
}