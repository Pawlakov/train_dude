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
    private Station(Guid id, Location? location, string nameGerman)
    {
        this.Id = id;
        this.Location = location;
        this.NameGerman = nameGerman;
    }

    public Guid Id { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public static Station Create(StationCreated e)
    {
        return new Station(e.StationId, null, e.NameGerman);
    }

    public void Apply(StationLocationSet e)
    {
        this.Location = e.Location;
    }
}