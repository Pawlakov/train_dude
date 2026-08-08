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
    private Station(Guid id, Location? location)
    {
        this.Id = id;
        this.Location = location;
    }

    public Guid Id { get; private set; }

    public Location? Location { get; private set; }

    public static Station Create(StationCreated e)
    {
        return new Station(e.StationId, null);
    }

    public void Apply(StationLocationSet e)
    {
        this.Location = e.Location;
    }
}