// <copyright file="GetTripQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrip;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripQuery(Guid TripId)
    : ILookupQuery<GetTripQueryResult>
{
    public const string TypeRoute = "/trip";

    public string Route => TypeRoute;

    public Guid Id => this.TripId;
}