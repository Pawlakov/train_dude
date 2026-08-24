// <copyright file="AxleAddedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Stations;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Stations;
using TrainDude.Integration.Projections.Exceptions;
using TrainDude.Queries.Data.Documents;

public static class AxleAddedProjectionHandler
{
    public static Task Handle(StationAxleAddedIntegrationEvent @event, ILiteCollection<Station> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        existing.Version = @event.Version;

        repository.Update(existing);

        return Task.CompletedTask;
    }
}