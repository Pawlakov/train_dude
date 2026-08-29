// <copyright file="SettingsStationNameModeUpdated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Settings;

using System;

using TrainDude.Domain.Base;
using TrainDude.Shared;
using TrainDude.Shared.Enums;

public sealed record class SettingsStationNameModeUpdated(Guid Id, DateTime When, StationNameMode StationNameMode) : BaseAggregateEvent<SettingsAggregate>(Id, When);