// <copyright file="GetSegmentsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentsQuery;

using System.Collections.Generic;

public class GetSegmentsQueryResultItem
{
    public int SegmentId { get; init; }

    public string NameA { get; init; }

    public string NameB { get; init; }

    public double? Length { get; init; }

    public double? Haversine { get; init; }
}