// <copyright file="StationAxleAddedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Stations;

using System;

public sealed record class StationAxleAddedIntegrationEvent(Guid Id, long Version);