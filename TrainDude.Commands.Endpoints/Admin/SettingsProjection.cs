// <copyright file="SettingsProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Admin;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Documents;
using TrainDude.Domain.Events.Admin;

public partial class SettingsProjection
    : SingleStreamProjection<Settings, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, Settings settings) => settings.Apply(e.Data);

    public void Apply(IEvent<SettingsStationNameModeUpdated> e, Settings settings) => settings.Apply(e.Data);
}