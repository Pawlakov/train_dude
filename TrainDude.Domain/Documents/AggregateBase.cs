// <copyright file="AggregateBase.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Documents;

using System;

public abstract class AggregateBase
{
    public Guid Id { get; protected set; }

    public long Version { get; protected set; }
}