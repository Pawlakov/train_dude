// <copyright file="RadiusCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Radii;

using System;

public sealed record class RadiusCreated(Guid Id, int Speed, int Minimum) : IDomainEvent;