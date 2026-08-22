// <copyright file="Trip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Events.Trips;

public class Trip
    : AggregateBase
{
    [JsonConstructor]
    private Trip(Guid id, long version, int tripNumber)
    {
        this.Id = id;
        this.Version = version;

        this.TripNumber = tripNumber;
    }

    public Trip()
    {
    }

    public int TripNumber { get; private set; }

    public static TripCreated Make(Guid tripId, int tripNumber)
    {
        return new TripCreated(tripId, DateTime.UtcNow, tripNumber);
    }

    public void Apply(TripCreated e)
    {
        this.Id = e.Id;
        this.TripNumber = e.TripNumber;

        this.Version++;
    }
}