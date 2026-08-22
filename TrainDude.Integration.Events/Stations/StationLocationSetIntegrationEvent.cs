// <copyright file="StationLocationSetIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Stations;

using System;

using TrainDude.Shared.Values;

public sealed record class StationLocationSetIntegrationEvent(Guid Id, long Version, Location Location) : IVersionedEvent;