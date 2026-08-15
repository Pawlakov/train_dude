// <copyright file="Settings.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Shared.Notifications.Settings;
using TrainDude.Shared.Values;

public class Settings
    : Aggregate
{
    [JsonConstructor]
    private Settings(Guid id, StationNameMode stationNameMode)
        : base(id)
    {
        this.StationNameMode = stationNameMode;
    }

    public StationNameMode StationNameMode { get; private set; }

    public static Settings Create(Guid settingsId, StationNameMode stationNameMode)
    {
        var settings = new Settings(settingsId, stationNameMode);
        settings.AddEvent(new SettingsCreatedNotification(settingsId));
        return settings;
    }

    public void UpdateStationNameMode(StationNameMode stationNameMode)
    {
        this.AddEvent(new SettingsStationNameModeUpdatedNotification(this.Id, stationNameMode));
    }

    protected override void Apply(INotification notification)
    {
        switch (notification)
        {
            case SettingsStationNameModeUpdatedNotification e:
                this.StationNameMode = e.StationNameMode;
                break;
            default:
                throw new NotSupportedException("This event type is not meant for this aggregate.");
        }
    }
}