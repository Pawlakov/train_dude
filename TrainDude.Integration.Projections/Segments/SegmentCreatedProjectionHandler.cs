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
    public static Task Handle(SegmentCreatedIntegrationEvent @event, ILiteCollection<Segment> repository, CancellationToken cancellationToken = default)
    {
        var existing = repository.Find(x => x.SegmentId == @event.Id).FirstOrDefault();
        if (existing is null)
        {
            var readModel = new Segment()
            {
                SegmentId = @event.Id,
                Version = @event.Version,
                NominalLength = @event.NominalLength,
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