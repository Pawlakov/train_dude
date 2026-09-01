// <copyright file="StationReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.ReadModels;

using System;
using System.Text.Json.Serialization;

using TrainDude.Features.Stations.Domain.Events;
using TrainDude.Features.Stations.ReadModels.Events;
using TrainDude.Shared.Values;

public sealed class StationReadModel
{
    [JsonConstructor]
    private StationReadModel(Guid id, long version, int axleCount, Location? location, string name)
    {
        this.Id = id;
        this.Version = version;

        this.AxleCount = axleCount;
        this.Location = location;
        this.Name = name;
    }

    public StationReadModel()
    {
    }

    public Guid Id { get; private set; }

    public long Version { get; private set; }

    public int AxleCount { get; private set; }

    public Location? Location { get; private set; }

    public string Name { get; private set; }

    public void Apply(StationCreatedWithReferences e)
    {
        this.Id = e.Id;
        this.Name = e.Name;

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