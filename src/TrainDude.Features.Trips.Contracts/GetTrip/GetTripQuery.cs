// <copyright file="GetTripQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrip;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripQuery()
    : ILookupQuery<GetTripQueryResult>
{
    public const string TypeRoute = "/api/trips/{0}";

    public static string Route => TypeRoute;
}