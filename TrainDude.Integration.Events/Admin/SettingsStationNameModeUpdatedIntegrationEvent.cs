// <copyright file="SettingsStationNameModeUpdatedIntegrationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events.Admin;

using System;
using System.Collections.Generic;

using TrainDude.Integration.Values;

public sealed record class SettingsStationNameModeUpdatedIntegrationEvent(StationNameMode StationNameMode, Dictionary<Guid, string> NewNames);