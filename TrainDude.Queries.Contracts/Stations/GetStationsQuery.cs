// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Stations;

using Mediator;

using TrainDude.Queries.Contracts.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public sealed record class GetStationsQuery
    : BaseEntityListQuery<GetStationsQueryResult>
{
}