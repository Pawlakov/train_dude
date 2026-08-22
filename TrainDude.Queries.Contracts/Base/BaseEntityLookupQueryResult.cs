// <copyright file="BaseEntityLookupQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Base;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public abstract class BaseEntityLookupQueryResult
    : BasePolymorphicQueryResult, IMapQueryResult
{
    required public IReadOnlyList<Location> StationPoints { get; init; }

    required public IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; init; }
}