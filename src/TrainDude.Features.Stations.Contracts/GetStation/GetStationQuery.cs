// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStation;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationQuery(Guid StationId)
    : ILookupQuery<GetStationQueryResult>
{
    public const string TypeRoute = "/api/station";

    public string Route => TypeRoute;

    public Guid Id => this.StationId;
}