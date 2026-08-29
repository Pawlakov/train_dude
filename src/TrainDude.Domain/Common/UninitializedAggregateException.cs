// <copyright file="UninitializedAggregateException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Common;

using System;

using TrainDude.Domain.Base;

public sealed class UninitializedAggregateException
    : DomainException
{
    public string MethodName { get; }

    public UninitializedAggregateException(Type aggregateType, string methodName)
        : base($"This {aggregateType.Name} tried to execute a ({methodName}) method which is reserved for properly initialized Aggregates.")
    {
        this.MethodName = methodName;
    }

    public override ErrorKind StatusCode => ErrorKind.Conflict;
}