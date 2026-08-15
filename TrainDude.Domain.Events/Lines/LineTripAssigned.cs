// <copyright file="LineTripAssigned.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Lines;

using System;

public sealed record class LineTripAssigned(Guid Id, Guid TripId) : IDomainEvent;