// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

using TrainDude.Shared.Values;

public class Segment
{
    private Segment()
    {
    }

    public int SegmentId { get; private set; }

    public double? NominalLength { get; private set; }

    public SegmentStation A { get; private set; }

    public SegmentStation B { get; private set; }
}