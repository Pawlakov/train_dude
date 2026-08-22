// <copyright file="DomainException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Exceptions;

using System;

using Microsoft.AspNetCore.Http;

public abstract class DomainException
    : Exception
{
    public abstract int StatusCode { get; }

    protected DomainException(string message)
        : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}