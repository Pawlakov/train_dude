// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegment;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentQueryResponse(
    int Tracks,
    double NominalLength,
    double? Haversine,
    Guid AId,
    string AName,
    Guid BId,
    string BName,
    IReadOnlyList<GetSegmentQueryResponse.SegmentTrip> Trips)
    : ILookupQueryResponse
{
    public record struct SegmentTrip(Guid TripId, int Number);
}