// <copyright file="TripCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Domain.Events;

using System;

public sealed record TripCreated(Guid Id, DateTime When, int TripNumber);