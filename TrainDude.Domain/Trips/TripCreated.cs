// <copyright file="TripCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Trips;

using System;

using TrainDude.Domain.Base;

public sealed record class TripCreated(Guid Id, DateTime When, int TripNumber) : BaseAggregateEvent(Id, When);