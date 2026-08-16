// <copyright file="LineProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Lines;

public partial class LineProjection
    : SingleStreamProjection<Line, Guid>
{
    public void Apply(IEvent<LineCreated> e, Line line) => line.Apply(e.Data);

    public void Apply(IEvent<LineTripAssigned> e, Line line) => line.Apply(e.Data);

    public void Apply(IEvent<LineStationAppended> e, Line line) => line.Apply(e.Data);
}