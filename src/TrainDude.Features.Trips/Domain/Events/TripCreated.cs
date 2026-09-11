// <copyright file="TripCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Domain.Events;

using System;

public sealed record TripCreated(Guid TripId, string Who, int TripNumber)
    : ITripEvent
{
    public Guid Id => this.TripId;
}