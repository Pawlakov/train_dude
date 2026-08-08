// <copyright file="Trip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Commands.Data.Events;

public class Trip
{
    [JsonConstructor]
    private Trip(Guid id, int tripNumber)
    {
        this.Id = id;
        this.TripNumber = tripNumber;
    }

    public Guid Id { get; private set; }

    public int TripNumber { get; private set; }

    public static Trip Create(TripCreated e)
    {
        return new Trip(e.TripId, e.TripNumber);
    }
}