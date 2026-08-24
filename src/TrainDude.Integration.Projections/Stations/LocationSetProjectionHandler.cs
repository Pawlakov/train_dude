// <copyright file="LocationSetProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Stations;

using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Stations;
using TrainDude.Queries.Data.Documents;

public static class LocationSetProjectionHandler
{
    public static Task Handle(StationLocationSetIntegrationEvent @event, ILiteCollection<Station> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        existing.Version = @event.Version;
        existing.Location = @event.Location;

        repository.Update(existing);

        return Task.CompletedTask;
    }
}