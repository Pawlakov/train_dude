// <copyright file="GetNetworkGeoDataQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetNetworkGeoJsonQuery;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;
using TrainDude.Shared.Values;

public class GetNetworkQueryResult
    : BasePolymorphicQueryResponse
{
    public ICollection<Location> Stations { get; set; }

    public ICollection<GetNetworkQueryResultSegmentItem> Segments { get; set; }
}