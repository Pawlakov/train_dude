// <copyright file="Station.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Commands.Data.Events;
using TrainDude.Shared.Values;

public class Station
{
    [JsonConstructor]
    private Station(Guid id, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        this.Id = id;
        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public Guid Id { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static Station Create(StationCreated e)
    {
        return new Station(e.StationId, null, e.NameGerman, e.NameGermanNew, e.NamePolish, e.NameRussian);
    }

    public void Apply(StationLocationSet e)
    {
        this.Location = e.Location;
    }
}