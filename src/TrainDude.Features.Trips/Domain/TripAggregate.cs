// <copyright file="TripAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Domain;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Shared.Contracts.Trips.Domain.Events;

public class TripAggregate
{
    [JsonConstructor]
    private TripAggregate(Guid id, long version, int tripNumber)
    {
        this.Id = id;
        this.Version = version;

        this.TripNumber = tripNumber;
    }

    public TripAggregate()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

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