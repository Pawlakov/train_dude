// <copyright file="StationAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Domain.Events;

public class StationAggregate
    : IHasAlternativeNames
{
    [JsonConstructor]
    private StationAggregate(Guid id, long version, int axleCount, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
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
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int AxleCount { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static StationCreated Make(Guid id, string who, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        return new StationCreated(id, DateTime.UtcNow, who, nameGerman, nameGermanNew, namePolish, nameRussian);
    }

    public StationLocationSet SetLocation(string who, Location location)
    {
        return new StationLocationSet(this.Id, DateTime.UtcNow, who, location);
    }

    public StationAxleAdded AddAxle(string who)
    {
        return new StationAxleAdded(this.Id, DateTime.UtcNow, who);
    }

    public void Apply(StationCreated e)
    {
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