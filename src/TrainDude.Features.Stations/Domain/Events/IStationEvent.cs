// <copyright file="IStationEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;

public interface IStationEvent
    : IDomainEvent
{
    Guid StationId { get; }
}