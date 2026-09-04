// <copyright file="RadiusCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Domain.Events;

using System;

public sealed record RadiusCreated(Guid Id, DateTime When, int Speed, int Minimum);