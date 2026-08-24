// <copyright file="StationCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Stations;

using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Stations;
using TrainDude.Queries.Data.Documents;

public static class StationCreatedProjectionHandler
{
    public static Task Handle(StationCreatedIntegrationEvent @event, ILiteCollection<Station> repository)
    {
        var existing = repository.FindById(@event.Id);
        if (existing is not null && existing.Version >= @event.Version)
        {
            return Task.CompletedTask;
        }

        var readModel = new Station()
        {
            Id = @event.Id,
            Version = @event.Version,
            Name = @event.Name,
            Location = null,
        };

        repository.Upsert(readModel);

        return Task.CompletedTask;
    }
}