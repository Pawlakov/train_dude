// <copyright file="TripCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Trips;

using System;

public sealed record class TripCreated(Guid Id, int TripNumber) : IDomainEvent;