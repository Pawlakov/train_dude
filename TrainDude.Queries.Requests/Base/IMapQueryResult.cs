// <copyright file="IMapQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Collections.Generic;

using TrainDude.Integration.Values;
using TrainDude.Queries.Requests.Network;

public interface IMapQueryResult
{
    IReadOnlyList<Location> StationPoints { get; }

    IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; }
}