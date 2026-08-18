// <copyright file="TrainAssignedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Lines;

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Lines;
using TrainDude.Queries.Data.Documents;

public static class TrainAssignedProjectionHandler
{
    public static Task Handle(LineTripAssignedIntegrationEvent @event, ILiteCollection<Line> repository, ILiteCollection<Trip> tripRepository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.LineId == @event.Id).FirstOrDefault();
        if (existing is not null && @event.Version - existing.Version == 1L)
        {
            var trip = tripRepository.Find(x => x.TripId == @event.TripId).Single();
            var tripModel = new Line.LineTrip
            {
                TripId = trip.TripId,
                TripNumber = trip.TripNumber,
            };

            existing.Version = @event.Version;
            existing.Trips = existing.Trips.Append(tripModel).ToImmutableList();

            repository.Update(existing);
        }
        else
        {
            throw new Exception("I'm sure that Wolverine has a neat way of handling this.");
        }

        return Task.CompletedTask;
    }
}