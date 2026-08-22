// <copyright file="BaseAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Base;

using System;

public abstract class BaseAggregate
{
    public Guid Id { get; protected set; }

    public long Version { get; protected set; }
}