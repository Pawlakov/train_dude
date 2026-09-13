// <copyright file="ITripEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Trips.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;

public interface ITripEvent
    : IDomainEvent
{
    Guid TripId { get; }
}