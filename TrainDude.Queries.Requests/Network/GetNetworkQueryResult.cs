// <copyright file="GetNetworkQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Network;

using System.Collections.Generic;

using TrainDude.Integration.Values;
using TrainDude.Queries.Requests.Base;

public class GetNetworkQueryResult
    : BasePolymorphicQueryResult, IMapQueryResult
{
    required public IReadOnlyList<Location> StationPoints { get; init; }

    required public IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; init; }
}