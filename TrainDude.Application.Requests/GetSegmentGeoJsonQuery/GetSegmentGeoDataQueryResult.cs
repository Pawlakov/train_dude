// <copyright file="GetSegmentGeoDataQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentGeoJsonQuery;

using TrainDude.Application.Requests.Base;
using TrainDude.Shared.Values;

public class GetSegmentGeoDataQueryResult
    : BasePolymorphicResponse
{
    public ICollection<Location> Stations { get; set; }

    public GetSegmentGeoDataQueryResultSegmentItem Segment { get; set; }
}