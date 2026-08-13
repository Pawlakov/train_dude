// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Stations;

using Mediator;

using TrainDude.Queries.Requests.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public sealed record class GetStationsQuery
    : BaseEntityListQuery<GetStationsQueryResult>
{
}