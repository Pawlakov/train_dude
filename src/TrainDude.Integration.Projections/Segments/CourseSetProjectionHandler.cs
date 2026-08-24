// <copyright file="CourseSetProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Segments;

using System.Collections.Immutable;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Segments;
using TrainDude.Queries.Data.Documents;

public static class CourseSetProjectionHandler
{
    public static Task Handle(SegmentCourseSetIntegrationEvent @event, ILiteCollection<Segment> repository)
    {
        var existing = repository.GetByVersionedEvent(@event);
        if (existing == null)
        {
            return Task.CompletedTask;
        }

        existing.Version = @event.Version;
        existing.Haversine = @event.Haversine;
        existing.Course = @event.Course.ToImmutableList();

        repository.Update(existing);

        return Task.CompletedTask;
    }
}