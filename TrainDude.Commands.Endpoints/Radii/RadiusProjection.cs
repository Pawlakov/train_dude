// <copyright file="RadiusProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Radii;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Radii;

public partial class RadiusProjection
    : SingleStreamProjection<Radius, Guid>
{
    public void Apply(IEvent<RadiusCreated> e, Radius radius) => radius.Apply(e.Data);
}