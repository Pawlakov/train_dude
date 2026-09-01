// <copyright file="BaseEntityLookupQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public abstract class BaseEntityLookupQueryResult
    : IMapQueryResult
{
    public required IReadOnlyList<Location> StationPoints { get; init; }

    public required IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; init; }
}