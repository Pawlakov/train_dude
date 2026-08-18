// <copyright file="StationAppendedProjectionHandler.cs" company="Pawlakov">
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

public static class StationAppendedProjectionHandler
{
    public static Task Handle(LineStationAppendedIntegrationEvent @event, ILiteCollection<Line> repository, ILiteCollection<Station> stationRepository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.LineId == @event.Id).FirstOrDefault();
        if (existing is not null && @event.Version - existing.Version == 1L)
        {
            var station = stationRepository.Find(x => x.StationId == @event.StationId).Single(); // TODO This will cause a problem if for example station's location changes
            var stationModel = new Line.LineStation
            {
                StationId = station.StationId,
                Name = station.Name,
                Location = station.Location,
            };

            existing.Version = @event.Version;
            existing.Stations = existing.Stations.Append(stationModel).ToImmutableList();

            repository.Update(existing);
        }
        else
        {
            throw new Exception("I'm sure that Wolverine has a neat way of handling this.");
        }

        return Task.CompletedTask;
    }
}