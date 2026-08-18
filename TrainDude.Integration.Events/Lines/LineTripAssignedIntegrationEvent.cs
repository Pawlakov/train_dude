// <copyright file="LineTripAssignedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Lines;

using System;

public sealed record class LineTripAssignedIntegrationEvent(Guid Id, long Version, Guid TripId);