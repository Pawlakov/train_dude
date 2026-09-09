// <copyright file="StationAxleAdded.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Features.Shared.Base;

public sealed record StationAxleAdded(Guid StationId, DateTime When, string Who) : IDomainEvent
{
    public Guid Id => this.StationId;
}