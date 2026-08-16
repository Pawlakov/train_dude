// <copyright file="GetNetworkQueryResultSegmentItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Network;

using System.Collections.Generic;

using TrainDude.Integration.Values;

public class GetNetworkQueryResultSegmentItem
{
    required public Location ALocation { get; init; }

    required public Location BLocation { get; init; }

    required public ICollection<Location> Vertices { get; init; }
}