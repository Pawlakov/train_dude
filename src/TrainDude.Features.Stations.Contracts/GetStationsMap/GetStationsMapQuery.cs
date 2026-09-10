// <copyright file="GetStationsMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStationsMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;

public sealed record GetStationsMapQuery()
    : IMapQuery, IGeneralQuery<MapQueryResult>
{
    public const string TypeRoute = "/api/stations/map";

    public static string Route => TypeRoute;
}