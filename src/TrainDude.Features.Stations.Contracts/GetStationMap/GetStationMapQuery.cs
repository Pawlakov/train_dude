// <copyright file="GetStationMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Stations.Contracts.GetStationMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationMapQuery(Guid StationId)
    : IMapQuery
{
    public const string TypeRoute = "/api/station/map";

    public string Route => TypeRoute;

    public Guid Id => this.StationId;
}