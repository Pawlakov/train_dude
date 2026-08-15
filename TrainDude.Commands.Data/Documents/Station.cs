// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Stations;
using TrainDude.Shared.Values;

public class Station
    : Aggregate
{
    [JsonConstructor]
    private Station(Guid id, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
        : base(id)
    {
        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static Station Create(Guid stationId, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        var station = new Station(stationId, null, nameGerman, nameGermanNew, namePolish, nameRussian);
        station.AddEvent(new StationCreatedNotification(station.Id, station.NameGerman, station.NameGermanNew, station.NamePolish, station.NameRussian));
        return station;
    }

    public void SetLocation(Location location)
    {
        this.AddEvent(new StationLocationSetNotification(this.Id, location));
    }

    protected override void Apply(INotification notification)
    {
        switch (notification)
        {
            case StationLocationSetNotification e:
                this.Location = e.Location;
                break;
            default:
                throw new NotSupportedException("This event type is not meant for this aggregate.");
        }
    }
}