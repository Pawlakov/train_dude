// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed record StationLocationSet(Guid StationId, string Who, Location Location)
    : IStationEvent
{
    public Guid Id => this.StationId;
}