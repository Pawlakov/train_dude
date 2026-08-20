// <copyright file="RadiusCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Radii;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Radii;
using TrainDude.Queries.Data.Documents;

public static class RadiusCreatedProjectionHandler
{
    public static Task Handle(RadiusCreatedIntegrationEvent @event, ILiteCollection<Radius> repository)
    {
        var existing = repository.Find(x => x.Id == @event.Id).FirstOrDefault();
        if (existing is not null && existing.Version >= @event.Version)
        {
            return Task.CompletedTask;
        }

        var readModel = new Radius
        {
            Id = @event.Id,
            Version = @event.Version,
            Speed = @event.Speed,
            Minimum = @event.Minimum,
        };

        repository.Upsert(readModel);

        return Task.CompletedTask;
    }
}