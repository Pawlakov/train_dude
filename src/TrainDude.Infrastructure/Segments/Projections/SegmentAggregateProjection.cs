// <copyright file="SegmentAggregateProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Segments.Domain;
using TrainDude.Features.Segments.Domain.Events;

public sealed class SegmentAggregateProjection
    : SingleStreamProjection<SegmentAggregate, Guid>
{
    public void Apply(IEvent<SegmentCreated> e, SegmentAggregate segmentAggregate) => segmentAggregate.Apply(e.Data);

    public void Apply(IEvent<SegmentCourseSet> e, SegmentAggregate segmentAggregate) => segmentAggregate.Apply(e.Data);
}