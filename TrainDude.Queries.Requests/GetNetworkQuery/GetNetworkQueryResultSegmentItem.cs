// <copyright file="GetNetworkQueryResultSegmentItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetNetworkQuery;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public class GetNetworkQueryResultSegmentItem
{
    required public Location ALocation { get; init; }

    required public Location BLocation { get; init; }

    required public ICollection<Location> Vertices { get; init; }
}