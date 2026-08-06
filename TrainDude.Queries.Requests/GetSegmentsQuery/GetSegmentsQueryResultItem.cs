// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentsQuery;

public class GetSegmentsQueryResultItem
{
    required public int SegmentId { get; init; }

    required public string NameA { get; init; }

    required public string NameB { get; init; }

    required public double? Length { get; init; }

    required public double? Haversine { get; init; }
}