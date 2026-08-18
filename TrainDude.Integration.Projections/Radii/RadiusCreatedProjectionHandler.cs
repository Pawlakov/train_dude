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
    public static Task Handle(RadiusCreatedIntegrationEvent @event, ILiteCollection<Radius> repository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.RadiusId == @event.Id).FirstOrDefault();
        if (existing is null)
        {
            var readModel = new Radius
            {
                RadiusId = @event.Id,
                Version = @event.Version,
                Speed = @event.Speed,
                Minimum = @event.Minimum,
            };

            repository.Insert(readModel);
        }
        else
        {
            throw new Exception("I'm sure that Wolverine has a neat way of handling this.");
        }

        return Task.CompletedTask;
    }
}