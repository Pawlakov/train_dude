// <copyright file="AssignTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.AssignTrip;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AssignTripCommand(Guid TripId)
    : IUpdateCommand
{
    public const string TypeRoute = "/api/lines/{id}/assign-trip";

    public string Route => TypeRoute;
}