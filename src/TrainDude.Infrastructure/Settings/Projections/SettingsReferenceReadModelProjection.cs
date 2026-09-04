// <copyright file="SettingsReferenceReadModelProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Shared.ReadModels;

public sealed class SettingsReferenceReadModelProjection
    : SingleStreamProjection<SharedSettingsReference, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, SharedSettingsReference aggregate) => aggregate.Apply(e.Data);

    public void Apply(IEvent<SettingsNamingPolicySet> e, SharedSettingsReference aggregate) => aggregate.Apply(e.Data);
}