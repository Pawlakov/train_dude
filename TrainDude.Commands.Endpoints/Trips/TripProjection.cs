// <copyright file="TripProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Trips;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Trips;

public partial class TripProjection
    : SingleStreamProjection<Trip, Guid>
{
    public void Apply(IEvent<TripCreated> e, Trip trip) => trip.Apply(e.Data);
}