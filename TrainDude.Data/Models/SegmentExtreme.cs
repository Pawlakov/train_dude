// <copyright file="SegmentExtreme.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

public class SegmentExtreme
{
    public bool IsEnd { get; set; }

    public int StationId { get; set; }

    public virtual Station? Station { get; set; }

    public int SegmentId { get; set; }

    public virtual Segment? Segment { get; set; }
}