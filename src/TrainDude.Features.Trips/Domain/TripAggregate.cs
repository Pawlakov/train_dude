// <copyright file="TripAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Domain;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Trips.Domain.Events;

public class TripAggregate
{
    [JsonConstructor]
    private TripAggregate(Guid id, int tripNumber)
    {
        this.Id = id;

        this.TripNumber = tripNumber;
    }

    public TripAggregate()
    {
    }

    public Guid Id { get; private set; }

    public int TripNumber { get; private set; }

    public static TripCreated Make(Guid tripId, string who, int tripNumber)
    {
        return new TripCreated(tripId, who, tripNumber);
    }

    public void Apply(TripCreated e)
    {
        this.Id = e.Id;
        this.TripNumber = e.TripNumber;
    }
}