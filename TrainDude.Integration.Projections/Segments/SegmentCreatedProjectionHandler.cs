// <copyright file="SegmentCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Segments;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Segments;
using TrainDude.Queries.Data.Documents;

public static class SegmentCreatedProjectionHandler
{
    public static Task Handle(SegmentCreatedIntegrationEvent @event, ILiteCollection<Segment> repository, ILiteCollection<Station> stationRepository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.SegmentId == @event.Id).FirstOrDefault();
        if (existing is null)
        {
            var a = stationRepository.Find(x => x.StationId == @event.AId).Single();
            var aModel = new Segment.SegmentStation
            {
                StationId = a.StationId,
                Name = a.Name,
                Location = a.Location,
            };

            var b = stationRepository.Find(x => x.StationId == @event.BId).Single();
            var bModel = new Segment.SegmentStation
            {
                StationId = b.StationId,
                Name = b.Name,
                Location = b.Location,
            };

            var readModel = new Segment()
            {
                SegmentId = @event.Id,
                Version = @event.Version,
                NominalLength = @event.NominalLength,
                A = aModel,
                B = bModel,
            };

            repository.Insert(readModel);
        }
        else
        {
            throw new Exception("I'm sure that Wolverine has a neat way of handling this.");
        }

        return Task.CompletedTask;
    }
}