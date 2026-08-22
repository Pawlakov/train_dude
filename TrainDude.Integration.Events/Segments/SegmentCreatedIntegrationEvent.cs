// <copyright file="SegmentCreatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Segments;

using System;

using TrainDude.Shared.Values;

public sealed record class SegmentCreatedIntegrationEvent(Guid Id, long Version, double? NominalLength, SegmentCreatedIntegrationEvent.Station A, SegmentCreatedIntegrationEvent.Station B) : IVersionedEvent
{
    public sealed record class Station(Guid Id, string Name, Location? Location);
}