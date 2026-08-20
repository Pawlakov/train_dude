// <copyright file="ProjectionException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Exceptions;

using System;

public abstract class ProjectionException
    : Exception
{
    protected ProjectionException(string message)
        : base(message)
    {
    }

    protected ProjectionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}