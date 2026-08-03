// <copyright file="SegmentVertex.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Entities;

using System;

using TrainDude.Shared.Values;

public class SegmentVertex
{
    private SegmentVertex()
    {
    }

    internal SegmentVertex(int ordinalId, Location location)
    {
        if (ordinalId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ordinalId));
        }

        this.OrdinalId = ordinalId;
        this.Location = location;
    }

    public int OrdinalId { get; private set; }

    public int SegmentId { get; private set; }

    public Segment Segment { get; private set; }

    public Location Location { get; private set; }
}