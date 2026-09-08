// <copyright file="GetStationsMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStationsMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationsMapQuery() : IMapQuery
{
    public const string TypeRoute = "/api/stations/map";

    public string Route => TypeRoute;
}