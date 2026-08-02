// <copyright file="GetNetworkGeoDataQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

using TrainDude.Application.Requests.Base;
using TrainDude.Shared.Values;

public class GetNetworkGeoDataQueryResult
    : BasePolymorphicResponse
{
    public ICollection<Location> Stations { get; set; }

    public ICollection<GetNetworkGeoDataQueryResultSegmentItem> Segments { get; set; }
}