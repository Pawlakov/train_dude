// <copyright file="SegmentExtreme.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Data.Entities;

public class SegmentExtreme
{
    private SegmentExtreme()
    {
    }

    internal SegmentExtreme(bool isEnd, Station sation)
    {
        this.IsEnd = isEnd;
        this.Station = sation;
    }

    public bool IsEnd { get; private set; }

    public int StationId { get; private set; }

    public Station Station { get; private set; }

    public int SegmentId { get; private set; }

    public Segment Segment { get; private set; }
}