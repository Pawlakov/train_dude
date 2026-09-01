// <copyright file="TripProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Trips.Domain;
using TrainDude.Features.Trips.Domain.Events;

public partial class TripProjection
    : SingleStreamProjection<TripAggregate, Guid>
{
    public void Apply(IEvent<TripCreated> e, TripAggregate aggregate) => aggregate.Apply(e.Data);
}