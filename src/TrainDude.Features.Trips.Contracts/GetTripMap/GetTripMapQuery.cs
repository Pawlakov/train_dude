// <copyright file="GetTripMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTripMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripMapQuery(Guid TripId)
    : IMapQuery
{
    public const string TypeRoute = "/api/trip/map";

    public string Route => TypeRoute;

    public Guid Id => this.TripId;
}