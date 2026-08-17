// <copyright file="DomainException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Exceptions;

using System;

using Microsoft.AspNetCore.Http;

public abstract class DomainException
    : Exception
{
    public string Code { get; }

    public virtual int StatusCode => StatusCodes.Status400BadRequest;

    protected DomainException(string code, string message)
        : base(message)
    {
        this.Code = code;
    }

    protected DomainException(string code, string message, Exception innerException)
        : base(message, innerException)
    {
        this.Code = code;
    }
}