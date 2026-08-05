// <copyright file="SegmentAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Data.Entities;

using TrainDude.Shared.Values;

public class SegmentAggregate
{
    private SegmentAggregate()
    {
    }

    public SegmentAggregate(int segmentId, double? nominalLength)
    {
        this.SegmentId = segmentId;
        this.NominalLength = nominalLength;
    }

    public int SegmentId { get; private set; }

    public double? NominalLength { get; private set; }

    public StationEntity A { get; private set; }

    public StationEntity B { get; private set; }

    public void SetA(int startStationStationId, string startStationNameGerman, string? startStationNameGermanNew, Location? startStationLocation)
    {
        this.A = new StationEntity(startStationStationId, startStationNameGerman, startStationNameGermanNew, startStationLocation);
    }

    public void SetB(int endStationStationId, string endStationNameGerman, string? endStationNameGermanNew, Location? endStationLocation)
    {
        this.B = new StationEntity(endStationStationId, endStationNameGerman, endStationNameGermanNew, endStationLocation);
    }
}