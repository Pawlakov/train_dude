// <copyright file="GetStationMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Contracts.GetStationMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationMapQuery()
    : IMapQuery
{
    public const string TypeRoute = "/api/stations/{id}/map";

    public string Route => TypeRoute;
}