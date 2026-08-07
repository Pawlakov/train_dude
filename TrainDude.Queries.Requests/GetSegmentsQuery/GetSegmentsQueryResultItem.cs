// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentsQuery;

public class GetSegmentsQueryResultItem
{
    required public int SegmentId { get; init; }

    required public string AName { get; init; }

    required public string BName { get; init; }

    required public double? Length { get; init; }

    required public double? Haversine { get; init; }
}