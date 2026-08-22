// <copyright file="TripProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Trips;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Trips;

public partial class TripProjection
    : SingleStreamProjection<TripAggregate, Guid>
{
    public void Apply(IEvent<TripCreated> e, TripAggregate aggregate) => aggregate.Apply(e.Data);
}