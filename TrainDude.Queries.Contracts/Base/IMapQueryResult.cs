// <copyright file="IMapQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Base;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public interface IMapQueryResult
{
    IReadOnlyList<Location> StationPoints { get; }

    IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; }
}