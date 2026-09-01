// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using System;
using System.Collections.Generic;

using TrainDude.Features.Base;

public class GetSegmentQueryResult
    : BaseEntityLookupQueryResult
{
    public required int Tracks { get; init; }

    public required double NominalLength { get; init; }

    public required double? Haversine { get; init; }

    public required SegmentEnd A { get; init; }

    public required SegmentEnd B { get; init; }

    public required IEnumerable<SegmentTrip> Trips { get; init; }

    public class SegmentEnd
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }
    }

    public class SegmentTrip
    {
        public required Guid Id { get; init; }

        public required int Number { get; init; }
    }
}