// <copyright file="SettingsProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Settings;

using System;

using JasperFx.Events;

using Marten.Events.Aggregation;

using TrainDude.Domain.Settings;

public partial class SettingsProjection
    : SingleStreamProjection<SettingsDocument, Guid>
{
    public void Apply(IEvent<SettingsCreated> e, SettingsDocument settingsDocument) => settingsDocument.Apply(e.Data);

    public void Apply(IEvent<SettingsStationNameModeUpdated> e, SettingsDocument settingsDocument) => settingsDocument.Apply(e.Data);
}