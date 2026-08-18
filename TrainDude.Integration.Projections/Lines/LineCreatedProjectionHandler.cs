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
    public static Task Handle(LineCreatedIntegrationEvent @event, ILiteCollection<Line> repository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.LineId == @event.Id).FirstOrDefault();
        if (existing is null)
        {
            var readModel = new Line()
            {
                LineId = @event.Id,
                Version = @event.Version,
                LineNumber = @event.Number,
                LineLetter = @event.Letter,
                LineDesignation = $"{@event.Number}{@event.Letter}",
                Trips = [],
                Stations = [],
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