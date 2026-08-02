// <copyright file="GetNetworkGeoDataQueryResultSegmentItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

using TrainDude.Shared.Values;

public class GetNetworkGeoDataQueryResultSegmentItem
{
    public Location ALocation { get; set; }

    public Location BLocation { get; set; }

    public ICollection<Location> Vertices { get; set; }
}