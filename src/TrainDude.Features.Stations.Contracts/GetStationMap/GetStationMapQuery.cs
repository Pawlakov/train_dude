// <copyright file="GetStationMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Contracts.GetStationMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;

public sealed record GetStationMapQuery()
    : IMapQuery, ISpecificQuery<MapQueryResult>
{
    public const string TypeRoute = "/api/stations/{id}/map";

    public static string Route => TypeRoute;
}