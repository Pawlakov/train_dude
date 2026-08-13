// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Stations;

using TrainDude.Queries.Requests.Base;

public class GetStationQueryResult
    : BaseEntityLookupQueryResult
{
    required public string Name { get; init; }
}