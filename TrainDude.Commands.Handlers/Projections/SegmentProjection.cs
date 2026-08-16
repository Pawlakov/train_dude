// <copyright file="SegmentProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Segments;

public partial class SegmentProjection
    : SingleStreamProjection<Segment, Guid>
{
    public void Apply(IEvent<SegmentCreated> e, Segment segment) => segment.Apply(e.Data);
}