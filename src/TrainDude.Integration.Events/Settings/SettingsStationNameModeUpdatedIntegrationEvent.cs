// <copyright file="SettingsStationNameModeUpdatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Settings;

using System;
using System.Collections.Generic;

using TrainDude.Shared;
using TrainDude.Shared.Enums;

public sealed record class SettingsStationNameModeUpdatedIntegrationEvent(StationNameMode StationNameMode, Dictionary<Guid, string> NewNames);