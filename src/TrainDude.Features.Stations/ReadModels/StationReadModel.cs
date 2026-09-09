// <copyright file="StationReadModel.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.ReadModels;

using System;

using TrainDude.Features.Shared.Contracts.Values;

public sealed class StationReadModel
{
    public Guid Id { get; set; }

    public long Version { get; set; }

    public int AxleCount { get; set; }

    public Location? Location { get; set; }

    public string Name { get; set; }
}