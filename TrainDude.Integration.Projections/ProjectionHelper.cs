// <copyright file="ProjectionHelper.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections;

using LiteDB;

using TrainDude.Integration.Events;
using TrainDude.Integration.Projections.Exceptions;
using TrainDude.Queries.Data;

internal static class ProjectionHelper
{
    internal static TReadModel? GetByVersionedEvent<TReadModel>(this ILiteCollection<TReadModel> repository, IVersionedEvent @event)
        where TReadModel : class, IVersionedDocument
    {
        var existing = repository.FindById(@event.Id);
        if (existing is null)
        {
            throw new ProjectionOutOfOrderException(@event.Version, null);
        }

        if (@event.Version <= existing.Version)
        {
            return null;
        }

        if (@event.Version - existing.Version != 1L)
        {
            throw new ProjectionOutOfOrderException(@event.Version, existing.Version);
        }

        return existing;
    }
}