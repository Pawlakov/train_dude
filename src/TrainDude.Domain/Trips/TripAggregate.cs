// <copyright file="TripAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Trips;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;

public class TripAggregate
    : BaseAggregate
{
    [JsonConstructor]
    private TripAggregate(Guid id, long version, int tripNumber)
        : base(id, version)
    {
        this.TripNumber = tripNumber;
    }

    public TripAggregate()
        : base()
    {
    }

    public int TripNumber { get; private set; }

    public static TripCreated Make(Guid tripId, int tripNumber)
    {
        return new TripCreated(tripId, DateTime.UtcNow, tripNumber);
    }

    public void Apply(BaseAggregateEvent<TripAggregate> @event)
    {
        switch (@event)
        {
            case TripCreated e:
                this.Id = e.Id;
                this.TripNumber = e.TripNumber;
                break;
            default:
                throw new NotSupportedException("Unknown event type.");
        }

        this.Version++;
    }
}