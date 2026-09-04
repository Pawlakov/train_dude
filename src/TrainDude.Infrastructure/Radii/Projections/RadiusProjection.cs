// <copyright file="RadiusProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Radii.Domain.Events;

public partial class RadiusProjection
    : SingleStreamProjection<RadiusAggregate, Guid>
{
    public void Apply(IEvent<RadiusCreated> e, RadiusAggregate aggregate) => aggregate.Apply(e.Data);
}