// <copyright file="StationAppendedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Lines;

using System.Linq;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Lines;
using TrainDude.Integration.Projections.Exceptions;
using TrainDude.Queries.Data.Documents;

public static class StationAppendedProjectionHandler
{
    public static Task Handle(LineStationAppendedIntegrationEvent @event, ILiteCollection<Line> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        var stationModel = new Line.LineStation
        {
            StationId = @event.Appended.Id,
            Name = @event.Appended.Name,
            Location = @event.Appended.Location,
        };

        existing.Version = @event.Version;
        existing.Stations = [.. existing.Stations, stationModel];

        repository.Update(existing);

        return Task.CompletedTask;
    }
}