// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Stations;
using TrainDude.Integration.Values;

public class Station
    : AggregateBase
{
    [JsonConstructor]
    private Station(Guid id, long version, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        this.Id = id;
        this.Version = version;

        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public Station()
    {
    }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static StationCreated Make(Guid stationId, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        return new StationCreated(stationId, nameGerman, nameGermanNew, namePolish, nameRussian);
    }

    public StationLocationSet SetLocation(Location location)
    {
        return new StationLocationSet(this.Id, location);
    }

    public StationAxleAdded AddAxle()
    {
        return new StationAxleAdded(this.Id);
    }

    public void Apply(StationCreated e)
    {
        this.Id = e.Id;
        this.Location = null;
        this.NameGerman = e.NameGerman;
        this.NameGermanNew = e.NameGermanNew;
        this.NamePolish = e.NamePolish;
        this.NameRussian = e.NameRussian;

        this.Version++;
    }

    public void Apply(StationLocationSet e)
    {
        this.Location = e.Location;

        this.Version++;
    }

    public void Apply(StationAxleAdded e)
    {
        throw new NotImplementedException();

        this.Version++;
    }
}