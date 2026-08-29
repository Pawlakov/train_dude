// <copyright file="SegmentAppendedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Lines;

using System.Linq;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Lines;
using TrainDude.Queries.Data.Documents;

public static class SegmentAppendedProjectionHandler
{
    public static Task Handle(LineSegmentAppendedIntegrationEvent @event, ILiteCollection<Line> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        existing.Version = @event.Version;
        existing.Stations = @event.Stations.Select(x => new Line.LineStation
        {
            StationId = x.Id,
            Name = x.Name,
            Location = x.Location,
        });

        repository.Update(existing);

        return Task.CompletedTask;
    }
}