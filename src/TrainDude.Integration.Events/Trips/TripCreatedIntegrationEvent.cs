// <copyright file="TripCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Trips;

using System;

public sealed record class TripCreatedIntegrationEvent(Guid Id, long Version, int Number) : IVersionedEvent;