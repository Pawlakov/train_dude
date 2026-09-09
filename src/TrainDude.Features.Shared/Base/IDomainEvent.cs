// <copyright file="IDomainEvent.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Base;

using System;

public interface IDomainEvent
{
    Guid Id { get; }

    DateTime When { get; }

    string Who { get; }
}