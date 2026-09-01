// <copyright file="DomainException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Exceptions;

using System;

public abstract class DomainException
    : Exception
{
    public abstract ErrorKind StatusCode { get; }

    protected DomainException(string message)
        : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}