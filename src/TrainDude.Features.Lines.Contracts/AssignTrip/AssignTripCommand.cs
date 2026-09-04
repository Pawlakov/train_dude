// <copyright file="AssignTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.AssignTrip;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record AssignTripCommand(Guid LineId, long Version, Guid TripId)
    : IUpdateCommand
{
    public const string TypeRoute = "/line/trips/assign";

    public string Route => TypeRoute;

    public Guid Id => this.LineId;
}