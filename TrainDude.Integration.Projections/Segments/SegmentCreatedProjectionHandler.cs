// <copyright file="SegmentCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Segments;

using System;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Segments;
using TrainDude.Queries.Data.Documents;

public static class SegmentCreatedProjectionHandler
{
    public static Task Handle(SegmentCreatedIntegrationEvent @event, ILiteCollection<Segment> segmentRepository)
    {
        var existing = segmentRepository.FindById(@event.Id);
        if (existing is not null && existing.Version >= @event.Version)
        {
            return Task.CompletedTask;
        }

        var readModel = new Segment()
        {
            Id = @event.Id,
            Version = @event.Version,
            NominalLength = @event.NominalLength,
            A = LoadStationModel(@event.A),
            B = LoadStationModel(@event.B),
        };

        segmentRepository.Upsert(readModel);

        return Task.CompletedTask;
    }

    private static Segment.SegmentStation LoadStationModel(SegmentCreatedIntegrationEvent.Station station)
    {
        return new Segment.SegmentStation
        {
            StationId = station.Id,
            Name = station.Name,
            Location = station.Location,
        };
    }
}