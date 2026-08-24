// <copyright file="StationCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Stations;

using System;

public sealed record class StationCreatedIntegrationEvent(Guid Id, long Version, string Name) : IVersionedEvent;