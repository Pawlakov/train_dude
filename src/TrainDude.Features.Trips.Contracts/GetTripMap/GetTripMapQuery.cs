// <copyright file="GetTripMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTripMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripMapQuery()
    : IMapQuery
{
    public const string TypeRoute = "/api/trips/{id}/map";

    public string Route => TypeRoute;
}