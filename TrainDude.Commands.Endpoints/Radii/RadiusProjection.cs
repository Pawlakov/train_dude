// <copyright file="RadiusProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Radii;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Radii;

public partial class RadiusProjection
    : SingleStreamProjection<RadiusAggregate, Guid>
{
    public void Apply(IEvent<RadiusCreated> e, RadiusAggregate aggregate) => aggregate.Apply(e.Data);
}