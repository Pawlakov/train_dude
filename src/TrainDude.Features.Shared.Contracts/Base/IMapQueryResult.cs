// <copyright file="IMapQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System.Collections.Generic;

using TrainDude.Shared.Values;

public interface IMapQueryResult
    : IQueryResult
{
    IReadOnlyList<Location> StationPoints { get; }

    IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings { get; }
}