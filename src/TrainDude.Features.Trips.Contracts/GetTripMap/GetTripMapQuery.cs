// <copyright file="GetTripMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTripMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;

public sealed record GetTripMapQuery()
    : IMapQuery, ISpecificQuery<MapQueryResult>
{
    public const string TypeRoute = "/api/trips/{id}/map";

    public static string Route => TypeRoute;
}