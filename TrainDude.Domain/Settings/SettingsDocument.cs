// <copyright file="SettingsDocument.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Settings;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;
using TrainDude.Shared.Values;

public class SettingsDocument
    : BaseAggregate
{
    [JsonConstructor]
    private SettingsDocument(Guid id, long version, StationNameMode stationNameMode)
    {
        this.Id = id;
        this.Version = version;

        this.StationNameMode = stationNameMode;
    }

    public SettingsDocument()
    {
    }

    public StationNameMode StationNameMode { get; private set; }

    public static SettingsCreated Make(Guid settingsId)
    {
        return new SettingsCreated(settingsId, DateTime.UtcNow);
    }

    public SettingsStationNameModeUpdated UpdateStationNameMode(StationNameMode stationNameMode)
    {
        return new SettingsStationNameModeUpdated(this.Id, DateTime.UtcNow, stationNameMode);
    }

    public void Apply(SettingsCreated e)
    {
        this.Id = e.Id;
        this.StationNameMode = StationNameMode.Modern;

        this.Version++;
    }

    public void Apply(SettingsStationNameModeUpdated e)
    {
        this.StationNameMode = e.StationNameMode;

        this.Version++;
    }
}