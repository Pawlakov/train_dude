// <copyright file="GetStationQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStation;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetStationQuery(Guid Id)
    : ILookupQuery<GetStationQueryResult>
{
    public const string TypeRoute = "/api/stations/{id}";

    public static string Route => TypeRoute;
}