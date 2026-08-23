// <copyright file="SegmentCourseSetIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Segments;

using System;
using System.Collections.Generic;

using TrainDude.Shared.Values;

public sealed record class SegmentCourseSetIntegrationEvent(Guid Id, long Version, IEnumerable<Location> Course, double? Haversine) : IVersionedEvent;