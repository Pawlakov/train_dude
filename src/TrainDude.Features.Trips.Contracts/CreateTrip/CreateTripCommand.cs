// <copyright file="CreateTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.CreateTrip;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateTripCommand(Guid TripId, int Number)
    : IGeneralCommand
{
    public const string TypeRoute = "/api/trips/create";

    public static string Route => TypeRoute;
}