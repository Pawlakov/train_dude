// <copyright file="SegmentProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Segments;

public partial class SegmentProjection
    : SingleStreamProjection<SegmentAggregate, Guid>
{
    public void Apply(IEvent<SegmentCreated> e, SegmentAggregate segmentAggregate) => segmentAggregate.Apply(e.Data);
}