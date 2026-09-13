// <copyright file="IRadiusEvents.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Radii.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;

public interface IRadiusEvents
    : IDomainEvent
{
    Guid RadiusId { get; }
}