// <copyright file="CreateTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.CreateTrip;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateTripCommand(int Number)
    : ICreateCommand
{
    public const string TypeRoute = "/api/trips/create";

    public string Route => TypeRoute;
}