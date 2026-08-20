// <copyright file="IVersionedEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Events;

using System;

public interface IVersionedEvent
{
    Guid Id { get; }

    long Version { get; }
}