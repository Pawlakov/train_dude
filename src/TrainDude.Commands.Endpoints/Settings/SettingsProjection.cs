// <copyright file="SettingsProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Settings;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Settings;

public partial class SettingsProjection
    : SingleStreamProjection<SettingsAggregate, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, SettingsAggregate settingsAggregate) => settingsAggregate.Apply(e.Data);

    public void Apply(IEvent<SettingsStationNameModeUpdated> e, SettingsAggregate settingsAggregate) => settingsAggregate.Apply(e.Data);
}