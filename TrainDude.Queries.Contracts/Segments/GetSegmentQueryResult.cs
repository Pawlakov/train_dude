// <copyright file="GetSegmentQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Segments;

using System;

using TrainDude.Queries.Contracts.Base;

public class GetSegmentQueryResult
    : BaseEntityLookupQueryResult
{
    public required int Tracks { get; set; }

    public required double NominalLength { get; set; }

    public required double? Haversine { get; set; }

    public required Guid AId { get; init; }

    public required string AName { get; init; }

    public required Guid BId { get; init; }

    public required string BName { get; init; }
}