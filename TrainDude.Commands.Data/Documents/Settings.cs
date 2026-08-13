// <copyright file="Settings.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Shared.Values;

public class Settings
{
    [JsonConstructor]
    private Settings(Guid id, StationNameMode stationNameMode)
    {
        this.Id = id;
        this.StationNameMode = stationNameMode;
    }

    public Guid Id { get; private set; }

    public StationNameMode StationNameMode { get; private set; }

    public static Settings Create(Guid id, StationNameMode stationNameMode)
    {
        return new Settings(id, stationNameMode);
    }

    public void UpdateStationNameMode(StationNameMode stationNameMode)
    {
        this.StationNameMode = stationNameMode;
    }
}