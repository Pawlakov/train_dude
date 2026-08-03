// <copyright file="LineSegment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Entities;

public class LineSegment
{
    private LineSegment()
    {
    }

    public int LineNumber { get; private set; }

    public char? LineLetter { get; private set; }

    public virtual Line Line { get; private set; }

    public int SegmentId { get; private set; }

    public Segment Segment { get; private set; }
}