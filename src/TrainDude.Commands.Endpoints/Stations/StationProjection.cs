// <copyright file="StationProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Stations;

public partial class StationProjection
    : SingleStreamProjection<StationAggregate, Guid>
{
    public void Apply(IEvent<StationCreated> e, StationAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationLocationSet> e, StationAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationAxleAdded> e, StationAggregate aggregate) => aggregate.Apply(e.Data);
}