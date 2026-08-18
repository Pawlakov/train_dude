// <copyright file="RadiusCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Radii;

using System;

public sealed record class RadiusCreatedIntegrationEvent(Guid Id, long Version, int Speed, int Minimum);