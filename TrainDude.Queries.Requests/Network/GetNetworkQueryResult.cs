// <copyright file="GetNetworkQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Network;

using System.Collections.Generic;

using TrainDude.Queries.Requests.Base;
using TrainDude.Shared.Values;

public class GetNetworkQueryResult
    : BasePolymorphicQueryResult
{
    required public ICollection<Location> Stations { get; init; }

    required public ICollection<GetNetworkQueryResultSegmentItem> Segments { get; init; }
}