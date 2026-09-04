// <copyright file="SettingsAggregateProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Settings.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Settings.Domain;
using TrainDude.Features.Settings.Domain.Events;

public sealed class SettingsAggregateProjection
    : SingleStreamProjection<SettingsAggregate, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, SettingsAggregate aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<SettingsNamingPolicySet> e, SettingsAggregate aggregate) => aggregate.Apply(e.Data);
}