// <copyright file="RadiusCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Radii;

using System;

using TrainDude.Domain.Base;

public sealed record class RadiusCreated(Guid Id, DateTime When, int Speed, int Minimum) : BaseAggregateEvent(Id, When);