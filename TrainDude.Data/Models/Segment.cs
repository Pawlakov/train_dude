// <copyright file="Segment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Models;

using System.Collections.Generic;

public class Segment
{
    public int SegmentId { get; set; }

    public double? NominalLength { get; set; }

    public virtual ICollection<SegmentExtreme> Extremes { get; set; }

    public virtual ICollection<SegmentVertexLocation> Vertices { get; set; }

    public virtual ICollection<ChartSegment> Charts { get; set; }
}