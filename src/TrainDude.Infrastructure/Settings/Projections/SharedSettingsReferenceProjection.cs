// <copyright file="SharedSettingsReferenceProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Settings.Projections;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Features.Settings.Domain.Events;
using TrainDude.Features.Shared.ReadModels;
using TrainDude.Shared.Enums;

public sealed class SharedSettingsReferenceProjection
    : SingleStreamProjection<SharedSettingsReference, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, SharedSettingsReference aggregate)
    {
        aggregate.Id = e.Data.Id;
        aggregate.NamingPolicy = NamingPolicy.Modern;

        aggregate.Version++;
    }

    public void Apply(IEvent<SettingsNamingPolicySet> e, SharedSettingsReference aggregate)
    {
        aggregate.NamingPolicy = e.Data.NamingPolicy;

        aggregate.Version++;
    }
}