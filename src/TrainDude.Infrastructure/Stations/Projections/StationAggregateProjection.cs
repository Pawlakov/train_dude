// <copyright file="StationAggregateProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Stations.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Stations.Domain;
using TrainDude.Features.Stations.Domain.Events;

public class StationAggregateProjection
    : SingleStreamProjection<StationAggregate, Guid>
{
    public void Apply(IEvent<StationCreated> e, StationAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationLocationSet> e, StationAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<StationAxleAdded> e, StationAggregate aggregate) => aggregate.Apply(e.Data);
}