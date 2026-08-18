// <copyright file="LineStationAppendedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Lines;

using System;

public sealed record class LineStationAppendedIntegrationEvent(Guid Id, long Version, Guid StationId);