// <copyright file="SegmentStationReference.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Stations.Domain.Events;
using TrainDude.Shared.Values;

public sealed class SegmentStationReference
    : IHasAlternativeNames
{
    [JsonConstructor]
    public SegmentStationReference(Guid id, long version, Location? location, string nameGerman, string? nameGermanNew, string? namePolish, string? nameRussian)
    {
        this.Id = id;
        this.Version = version;

        this.Location = location;
        this.NameGerman = nameGerman;
        this.NameGermanNew = nameGermanNew;
        this.NamePolish = namePolish;
        this.NameRussian = nameRussian;
    }

    public SegmentStationReference()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public Location? Location { get; private set; }

    public string NameGerman { get; private set; }

    public string? NameGermanNew { get; private set; }

    public string? NamePolish { get; private set; }

    public string? NameRussian { get; private set; }

    public void Apply(StationCreated e)
    {
        this.Id = e.Id;
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
        this.Version++;
    }
}