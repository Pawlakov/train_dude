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
    [JsonConstructor]
    private StationAggregate(Guid id, long version, int axleCount, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
        : base(id, version)
    {
        this.AxleCount = axleCount;
        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public StationAggregate()
        : base()
    {
    }

    public int AxleCount { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public static StationCreated Make(Guid id, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        return new StationCreated(id, DateTime.UtcNow, nameGerman, nameGermanNew, namePolish, nameRussian);
    }

    public StationLocationSet SetLocation(Location location)
    {
        this.AssertInitialized(nameof(this.AddAxle));
        return new StationLocationSet(this.Id, DateTime.UtcNow, location);
    }

    public StationAxleAdded AddAxle()
    {
        this.AssertInitialized(nameof(this.AddAxle));
        return new StationAxleAdded(this.Id, DateTime.UtcNow);
    }

    public void Apply(BaseDomainEvent<StationAggregate> @event)
    {
        switch (@event)
        {
            case StationCreated e:
                this.Initialize();
                this.Id = e.Id;
                this.Location = null;
                this.NameGerman = e.NameGerman;
                this.NameGermanNew = e.NameGermanNew;
                this.NamePolish = e.NamePolish;
                this.NameRussian = e.NameRussian;
                break;
            case StationLocationSet e:
                this.Location = e.Location;
                break;
            case StationAxleAdded e:
                this.AxleCount += 1;
                break;
            default:
                throw new NotSupportedException("Unknown event type.");
        }

        this.Version++;
    }
}