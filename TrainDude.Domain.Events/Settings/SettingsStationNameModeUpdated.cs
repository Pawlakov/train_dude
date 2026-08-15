// <copyright file="SettingsStationNameModeUpdated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Settings;

using System;

using TrainDude.Shared.Values;

public sealed record class SettingsStationNameModeUpdated(Guid Id, StationNameMode StationNameMode) : IDomainEvent;