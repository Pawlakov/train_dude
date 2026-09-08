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
    Guid AId,
    string AName,
    Guid BId,
    string BName,
    IReadOnlyList<GetSegmentQueryResult.SegmentTrip> Trips)
    : ILookupQueryResult
{
    public record struct SegmentTrip(Guid TripId, int Number);
}