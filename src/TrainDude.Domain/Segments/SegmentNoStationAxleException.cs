// <copyright file="SegmentNoStationAxleException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Segments;

using System;

using TrainDude.Domain.Base;
using TrainDude.Domain.Stations;

public sealed class SegmentNoStationAxleException
    : DomainException
{
    public Guid SegmentId { get; }

    public int AxleIndex { get; }

    public Guid StationId { get; }

    public int AxleCount { get; }

    public SegmentNoStationAxleException(Guid segmentId, int axleIndex, Guid stationId, int axleCount)
        : base($"Segment ({segmentId}) tried to link to the station ({stationId}) on axle {axleIndex} but the station has only {axleCount} axles.")
    {
        this.SegmentId = segmentId;
        this.AxleIndex = axleIndex;
        this.StationId = stationId;
        this.AxleCount = axleCount;
    }

    public override ErrorKind StatusCode => ErrorKind.NotFound;
}