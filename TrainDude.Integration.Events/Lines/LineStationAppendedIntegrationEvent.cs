// <copyright file="LineStationAppendedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Lines;

using System;

using TrainDude.Shared.Values;

public sealed record class LineStationAppendedIntegrationEvent(Guid Id, long Version, LineStationAppendedIntegrationEvent.Station Appended) : IVersionedEvent
{
    public sealed record class Station(Guid Id, string Name, Location? Location);
}