// <copyright file="TripCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Projections.Trips;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Trips;
using TrainDude.Queries.Data.Documents;

public static class TripCreatedProjectionHandler
{
    public static async Task Handle(TripCreatedIntegrationEvent @event, ILiteCollection<Trip> repository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.TripId == @event.Id).FirstOrDefault();
        if (existing is null || existing.Version < @event.Version)
        {
            var readModel = new Trip
            {
                TripId = @event.Id,
                TripNumber = @event.Number,
                Version = @event.Version,
            };

            repository.Upsert(readModel);
        }
    }
}