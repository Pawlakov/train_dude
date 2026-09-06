// <copyright file="GetTripsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrips;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripsQuery()
    : IListQuery<GetTripsQueryResult>
{
    public const string TypeRoute = "/api/trips";

    public string Route => TypeRoute;
}