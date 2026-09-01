// <copyright file="LineAggregateProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Lines.Domain.Events;

public partial class LineAggregateProjection
    : SingleStreamProjection<LineAggregate, Guid>
{
    public void Apply(IEvent<LineCreated> e, LineAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<LineTripAssigned> e, LineAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<LineSegmentAppended> e, LineAggregate aggregate) => aggregate.Apply(e.Data);
}