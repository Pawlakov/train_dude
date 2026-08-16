// <copyright file="Settings.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Admin;
using TrainDude.Integration.Values;

public class Settings
    : AggregateBase
{
    [JsonConstructor]
    private Settings(Guid id, long version, StationNameMode stationNameMode)
    {
        this.Id = id;
        this.Version = version;

        this.StationNameMode = stationNameMode;
    }

    public Settings()
    {
    }

    public StationNameMode StationNameMode { get; private set; }

    public static SettingsCreated Make(Guid settingsId, StationNameMode stationNameMode)
    {
        return new SettingsCreated(settingsId);
    }

    public SettingsStationNameModeUpdated UpdateStationNameMode(StationNameMode stationNameMode)
    {
        return new SettingsStationNameModeUpdated(this.Id, stationNameMode);
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