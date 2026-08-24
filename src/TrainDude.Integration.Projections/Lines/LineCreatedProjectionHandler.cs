// <copyright file="LineCreatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Lines;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Lines;
using TrainDude.Queries.Data.Documents;

public static class LineCreatedProjectionHandler
{
    public static Task Handle(LineCreatedIntegrationEvent @event, ILiteCollection<Line> repository)
    {
        var existing = repository.FindById(@event.Id);
        if (existing is not null && existing.Version >= @event.Version)
        {
            return Task.CompletedTask;
        }

        var readModel = new Line()
        {
            Id = @event.Id,
            Version = @event.Version,
            LineNumber = @event.Number,
            LineLetter = @event.Letter,
            LineDesignation = $"{@event.Number}{@event.Letter}",
            Trips = [],
            Stations = [],
        };

        repository.Upsert(readModel);

        return Task.CompletedTask;
    }
}