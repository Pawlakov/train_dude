// <copyright file="IDomainEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events;

using System;

public interface IDomainEvent
{
    public Guid Id { get; }
}