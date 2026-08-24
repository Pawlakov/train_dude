// <copyright file="TrainAssignedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Lines;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Lines;
using TrainDude.Integration.Projections.Exceptions;
using TrainDude.Queries.Data.Documents;

public static class TrainAssignedProjectionHandler
{
    public static Task Handle(LineTripAssignedIntegrationEvent @event, ILiteCollection<Line> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        var tripModel = new Line.LineTrip
        {
            TripId = @event.Assigned.Id,
            TripNumber = @event.Assigned.Number,
        };

        existing.Version = @event.Version;
        existing.Trips = [.. existing.Trips, tripModel];

        repository.Update(existing);

        return Task.CompletedTask;
    }
}