// <copyright file="ChartSegment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Data.Entities;

public class ChartSegment
{
    public string ChartId { get; set; }

    public virtual Chart Chart { get; set; }

    public int SegmentId { get; set; }

    public virtual Segment Segment { get; set; }
}