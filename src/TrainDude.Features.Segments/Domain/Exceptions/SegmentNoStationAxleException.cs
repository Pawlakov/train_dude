// <copyright file="SegmentNoStationAxleException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Domain.Exceptions;

using System;

using TrainDude.Features.Shared.Exceptions;

public sealed class SegmentNoStationAxleException
    : DomainException
{
    public int AxleIndex { get; }

    public Guid StationId { get; }

    public int AxleCount { get; }

    public SegmentNoStationAxleException(int axleIndex, Guid stationId, int axleCount)
        : base($"A new segment tried to link to the station ({stationId}) on axle {axleIndex} but the station has only {axleCount} axles.")
    {
        this.AxleIndex = axleIndex;
        this.StationId = stationId;
        this.AxleCount = axleCount;
    }

    public override ErrorKind StatusCode => ErrorKind.NotFound;
}