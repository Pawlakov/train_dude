// <copyright file="LineSegment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

public class LineSegment
{
    public string LineId { get; set; }

    public virtual Line Line { get; set; }

    public int SegmentId { get; set; }

    public virtual Segment Segment { get; set; }
}