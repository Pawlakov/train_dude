// <copyright file="RadiusCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Radii;

using System;

using TrainDude.Domain.Events.Base;

public sealed record class RadiusCreated(Guid Id, DateTime When, int Speed, int Minimum) : BaseAggregateEvent(Id, When);