// <copyright file="StationAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Stations;

using System;
using System.Text.Json.Serialization;

using TrainDude.Domain.Base;
using TrainDude.Shared;
using TrainDude.Shared.Values;

public class StationAggregate
    : BaseAggregate, IHasAlternativeNames
{
    private bool initialized;

    [JsonConstructor]
    private StationAggregate(Guid id, long version, int axleCount, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        this.initialized = true;
        this.Id = id;
        this.Version = version;

        this.AxleCount = axleCount;
        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public StationAggregate()
    {
        this.initialized = false;
    }

    public int AxleCount { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static StationCreated Make(Guid stationId, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        return new StationCreated(stationId, DateTime.UtcNow, nameGerman, nameGermanNew, namePolish, nameRussian);
    }

    public StationLocationSet SetLocation(Location location)
    {
        if (!this.initialized)
        {
            throw new UninitializedAggregateException<StationAggregate>(nameof(this.SetLocation));
        }

        return new StationLocationSet(this.Id, DateTime.UtcNow, location);
    }

    public StationAxleAdded AddAxle()
    {
        if (!this.initialized)
        {
            throw new UninitializedAggregateException<StationAggregate>(nameof(this.AddAxle));
        }

        return new StationAxleAdded(this.Id, DateTime.UtcNow);
    }

    public void Apply(StationCreated e)
    {
        this.initialized = true;

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
        this.AxleCount += 1;

        this.Version++;
    }
}