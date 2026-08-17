// <copyright file="AxleAddedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Handlers.Projections.Stations;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Stations;
using TrainDude.Queries.Data.Documents;

public static class AxleAddedProjectionHandler
{
    public static Task Handle(StationAxleAddedIntegrationEvent @event, ILiteCollection<Station> repository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.StationId == @event.Id).FirstOrDefault();
        if (existing is not null && @event.Version - existing.Version == 1L)
        {
            existing.Version = @event.Version;

            repository.Update(existing);
        }
        else
        {
            throw new Exception("I'm sure that Wolverine has a neat way of handling this.");
        }

        return Task.CompletedTask;
    }
}