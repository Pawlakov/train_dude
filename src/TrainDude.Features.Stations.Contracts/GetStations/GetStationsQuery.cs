// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStations;

using TrainDude.Features.Shared.Contracts.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public sealed record GetStationsQuery : IListQuery<GetStationsQueryResult>
{
    public const string TypeRoute = "/api/stations";

    public string Route => TypeRoute;
}