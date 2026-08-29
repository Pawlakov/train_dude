// <copyright file="BaseAggregate.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Base;

using System;

using TrainDude.Domain.Common;

public abstract class BaseAggregate
{
    private bool initialized;

    protected BaseAggregate(Guid id, long version)
    {
        this.initialized = true;
        this.Id = id;
        this.Version = version;
    }

    protected BaseAggregate()
    {
        this.initialized = false;
    }

    public Guid Id { get; protected set; }

    public long Version { get; protected set; }

    protected void AssertInitialized(string methodName)
    {
        if (!this.initialized)
        {
            throw new UninitializedAggregateException(this.GetType(), methodName);
        }
    }

    protected void Initialize()
    {
        this.initialized = true;
    }
}