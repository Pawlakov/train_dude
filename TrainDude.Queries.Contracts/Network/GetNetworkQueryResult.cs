// <copyright file="GetNetworkQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Network;

using System.Collections.Generic;

using TrainDude.Queries.Contracts.Base;
using TrainDude.Shared.Values;

public class GetNetworkQueryResult
    : BasePolymorphicQueryResult, IMapQueryResult
{
    required public IReadOnlyList<Location> StationPoints { get; init; }

    required public IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; init; }
}