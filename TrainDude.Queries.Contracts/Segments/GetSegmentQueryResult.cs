// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Segments;

using TrainDude.Queries.Contracts.Base;

public class GetSegmentQueryResult
    : BaseEntityLookupQueryResult
{
    required public string AName { get; init; }

    required public string BName { get; init; }
}