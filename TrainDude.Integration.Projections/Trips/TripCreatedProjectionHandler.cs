// <copyright file="TripCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Trips;

using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Trips;
using TrainDude.Queries.Data.Documents;

public static class TripCreatedProjectionHandler
{
    public static Task Handle(TripCreatedIntegrationEvent @event, ILiteCollection<Trip> repository)
    {
        var existing = repository.FindById(@event.Id);
        if (existing is not null && existing.Version >= @event.Version)
        {
            return Task.CompletedTask; // No accidental rollbacks allowed
        }

        var readModel = new Trip
        {
            Id = @event.Id,
            TripNumber = @event.Number,
            Version = @event.Version,
        };

        repository.Upsert(readModel); // projections should IDEMPOTENT

        return Task.CompletedTask;
    }
}