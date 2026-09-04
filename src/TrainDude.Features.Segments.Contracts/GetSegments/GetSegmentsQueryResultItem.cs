// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegments;

using System;

public class GetSegmentsQueryResultItem
{
    public required Guid SegmentId { get; init; }

    public required double? Length { get; init; }

    public required double? Haversine { get; init; }

    public required SegmentEnd A { get; init; }

    public required SegmentEnd B { get; init; }

    public class SegmentEnd
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }
    }
}