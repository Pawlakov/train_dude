// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Segments;

using System;

public class GetSegmentsQueryResultItem
{
    required public Guid SegmentId { get; init; }

    required public string AName { get; init; }

    required public string BName { get; init; }

    required public double? Length { get; init; }

    required public double? Haversine { get; init; }
}