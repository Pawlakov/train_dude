// <copyright file="LineSegmentAppendedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Lines;

using System;
using System.Collections.Generic;

using TrainDude.Shared.Values;

public sealed record class LineSegmentAppendedIntegrationEvent(Guid Id, long Version, IEnumerable<LineSegmentAppendedIntegrationEvent.Station> Stations, IEnumerable<LineSegmentAppendedIntegrationEvent.Segment> Segments) : IVersionedEvent
{
    public sealed record class Segment(Guid Id);

    public sealed record class Station(Guid Id, string Name, Location? Location);
}