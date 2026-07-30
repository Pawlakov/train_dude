// <copyright file="SegmentVertexLocation.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

public class SegmentVertexLocation
    : Location
{
    public int OrdinalId { get; set; }

    public int SegmentId { get; set; }

    public Segment Segment { get; set; }
}