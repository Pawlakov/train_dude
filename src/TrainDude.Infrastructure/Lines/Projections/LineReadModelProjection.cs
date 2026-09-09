// <copyright file="LineReadModelProjection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Lines.Projections;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;
using JasperFx.Events.Grouping;

using Marten;
using Marten.Events.Projections;

using TrainDude.Features.Lines.Domain.Events;
using TrainDude.Features.Lines.Domain.Values;
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Lines.ReadModels.Events;
using TrainDude.Infrastructure.Lines.Groupers;

public sealed class LineReadModelProjection
    : MultiStreamProjection<LineReadModel, Guid>
{
    public LineReadModelProjection()
    {
        this.Identity<LineCreated>(e => e.Id);
        this.Identity<LineTripAssigned>(e => e.Id);
        this.Identity<LineSegmentAppended>(e => e.Id);

        this.CustomGrouping(new LineReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<LineReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        var tripAssignedEvents = group.Slices
            .SelectMany(slice => slice.Events().OfType<IEvent<LineTripAssigned>>())
            .ToArray();

        if (tripAssignedEvents.Length == 0)
        {
            return;
        }

        var tripIds = tripAssignedEvents
            .Select(e => e.Data.TripId)
            .ToArray();

        var trips = await querySession.LoadManyAsync<LineTripReference>(cancellation, tripIds);

        var tripsById = trips.ToDictionary(s => s.Id, s => s);

        foreach (var slice in group.Slices)
        {
            foreach (var e in slice.Events().OfType<IEvent<LineTripAssigned>>().ToArray())
            {
                var reference = tripsById[e.Data.TripId];
                var trip = new LineTrip(reference.Id, reference.Number);
                var enriched = new LineTripAssignedWithReferences(e.Data.Id, e.Data.When, trip);

                slice.ReplaceEvent(e, enriched);
            }
        }
    }

    public void Apply(IEvent<LineCreated> e, LineReadModel readModel)
    {
        readModel.Id = e.Data.Id;
        readModel.LineNumber = e.Data.LineNumber;
        readModel.LineLetter = e.Data.LineLetter;
        readModel.LineDesignation = $"{e.Data.LineNumber}{e.Data.LineLetter}";

        readModel.Version++;
    }

    public void Apply(IEvent<LineTripAssignedWithReferences> e, LineReadModel readModel)
    {
        readModel.Trips = readModel.Trips.Append(e.Data.Trip).ToList();

        readModel.Version++;
    }

    public void Apply(IEvent<LineSegmentAppendedWithReferences> e, LineReadModel readModel)
    {
        if (e.Data.Segment.A == readModel.Stations.LastOrDefault())
        {
            readModel.Segments = readModel.Segments.Append(e.Data.Segment).ToList();
            readModel.Stations = readModel.Stations.Append(e.Data.Segment.B).ToList();
        }
        else
        {
            readModel.Segments = readModel.Segments.Append(e.Data.Segment).ToList();
            readModel.Stations = readModel.Stations.Append(e.Data.Segment.A).ToList();
        }

        readModel.Version++;
    }
}