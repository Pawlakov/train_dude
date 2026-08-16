// <copyright file="SettingsStationNameModeUpdated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Admin;

using System;

using TrainDude.Integration.Values;

public sealed record class SettingsStationNameModeUpdated(Guid Id, StationNameMode StationNameMode) : IDomainEvent;