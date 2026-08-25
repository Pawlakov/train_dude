// <copyright file="UninitializedAggregateException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Base;

public sealed class UninitializedAggregateException<TAggregate>
    : DomainException
{
    public string MethodName { get; }

    public UninitializedAggregateException(string methodName)
        : base($"This {typeof(TAggregate).Name} tried to execute a ({methodName}) method which is reserved for properly initialized Aggregates.")
    {
        this.MethodName = methodName;
    }

    public override ErrorKind StatusCode => ErrorKind.Conflict;
}