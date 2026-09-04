// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegment;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record GetSegmentQueryResult(
    int Tracks,
    double NominalLength,
    double? Haversine,
    GetSegmentQueryResult.SegmentEnd A,
    GetSegmentQueryResult.SegmentEnd B,
    IReadOnlyList<GetSegmentQueryResult.SegmentTrip> Trips,
    IReadOnlyList<Location> StationPoints,
    IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings)
    : ILookupQueryResult
{
    public record struct SegmentEnd(Guid StationId, string Name);

    public record struct SegmentTrip(Guid TripId, int Number);
}