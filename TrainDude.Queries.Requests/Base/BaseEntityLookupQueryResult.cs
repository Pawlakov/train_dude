// <copyright file="BaseEntityLookupQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Collections.Generic;

using TrainDude.Integration.Values;

public abstract class BaseEntityLookupQueryResult
    : BasePolymorphicQueryResult
{
    required public IEnumerable<Location> StationPoints { get; init; }

    required public IEnumerable<IEnumerable<Location>> SegmentLineStrings { get; init; }
}