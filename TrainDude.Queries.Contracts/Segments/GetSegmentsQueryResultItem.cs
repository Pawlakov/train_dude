// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Segments;

using System;

public class GetSegmentsQueryResultItem
{
    public required Guid SegmentId { get; init; }

    public required string AName { get; init; }

    public required string BName { get; init; }

    public required double? Length { get; init; }

    public required double? Haversine { get; init; }
}