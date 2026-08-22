// <copyright file="LineTripAssigned.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Lines;

using System;

using TrainDude.Domain.Base;

public sealed record class LineTripAssigned(Guid Id, DateTime When, Guid TripId) : BaseAggregateEvent(Id, When);