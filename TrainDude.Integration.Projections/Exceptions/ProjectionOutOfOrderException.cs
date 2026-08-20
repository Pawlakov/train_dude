// <copyright file="ProjectionOutOfOrderException.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Exceptions;

using System;

public sealed class ProjectionOutOfOrderException
    : ProjectionException
{
    public ProjectionOutOfOrderException(long eventVerion, long? existingVersion)
        : base($"Integration event arrived out of order. The event version was {eventVerion} while the existing version was {(existingVersion.HasValue ? existingVersion.Value : "missing")}.")
    {
    }
}