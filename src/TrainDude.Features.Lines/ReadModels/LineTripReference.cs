// <copyright file="LineTripReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Trips.Domain.Events;

public sealed class LineTripReference
{
    [JsonConstructor]
    public LineTripReference(Guid id, long version, int number)
    {
        this.Id = id;
        this.Version = version;
        this.Number = number;
    }

    public LineTripReference()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int Number { get; private set; }

    public void Apply(TripCreated e)
    {
        this.Id = e.Id;
        this.Number = e.TripNumber;

        this.Version++;
    }
}