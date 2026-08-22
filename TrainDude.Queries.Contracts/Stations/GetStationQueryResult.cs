// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Stations;

using TrainDude.Queries.Contracts.Base;

public class GetStationQueryResult
    : BaseEntityLookupQueryResult
{
    public required string Name { get; init; }
}