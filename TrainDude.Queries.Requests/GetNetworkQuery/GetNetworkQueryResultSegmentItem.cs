// <copyright file="GetNetworkQueryResultSegmentItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetNetworkQuery;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public class GetNetworkQueryResultSegmentItem
{
    public Location ALocation { get; set; }

    public Location BLocation { get; set; }

    public ICollection<Location> Vertices { get; set; }
}