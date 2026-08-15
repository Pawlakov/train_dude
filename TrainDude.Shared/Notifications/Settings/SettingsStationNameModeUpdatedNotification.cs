// <copyright file="SettingsStationNameModeUpdatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Settings;

using System;

using Mediator;

using TrainDude.Shared.Values;

public sealed record class SettingsStationNameModeUpdatedNotification(Guid SettingsId, StationNameMode StationNameMode) : INotification;