// <copyright file="SettingsAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Settings;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;
using TrainDude.Shared.Enums;

public class SettingsAggregate
    : BaseAggregate
{
    [JsonConstructor]
    private SettingsAggregate(Guid id, long version, StationNameMode stationNameMode)
        : base(id, version)
    {
        this.StationNameMode = stationNameMode;
    }

    public SettingsAggregate()
        : base()
    {
    }

    public StationNameMode StationNameMode { get; private set; }

    public static SettingsCreated Make(Guid settingsId)
    {
        return new SettingsCreated(settingsId, DateTime.UtcNow);
    }

    public SettingsStationNameModeUpdated UpdateStationNameMode(StationNameMode stationNameMode)
    {
        this.AssertInitialized(nameof(this.UpdateStationNameMode));
        return new SettingsStationNameModeUpdated(this.Id, DateTime.UtcNow, stationNameMode);
    }

    public void Apply(BaseAggregateEvent<SettingsAggregate> @event)
    {
        switch (@event)
        {
            case SettingsCreated e:
                this.Initialize();
                this.Id = e.Id;
                this.StationNameMode = StationNameMode.Modern;
                break;
            case SettingsStationNameModeUpdated e:
                this.StationNameMode = e.StationNameMode;
                break;
            default:
                throw new NotSupportedException("Unknown event type.");
        }

        this.Version++;
    }
}