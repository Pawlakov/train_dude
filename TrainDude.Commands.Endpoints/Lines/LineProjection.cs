// <copyright file="LineProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Lines;

public partial class LineProjection
    : SingleStreamProjection<LineAggregate, Guid>
{
    public void Apply(IEvent<LineCreated> e, LineAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<LineTripAssigned> e, LineAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<LineStationAppended> e, LineAggregate aggregate) => aggregate.Apply(e.Data);
}