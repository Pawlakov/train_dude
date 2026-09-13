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
using TrainDude.Features.Lines.ReadModels;
using TrainDude.Features.Lines.ReadModels.Values;
using TrainDude.Infrastructure.Lines.Events;
using TrainDude.Infrastructure.Lines.Groupers;

public sealed class LineReadModelProjection
    : MultiStreamProjection<LineReadModel, Guid>
{
    public LineReadModelProjection()
    {
        this.TransformsEvent<ILineEvent>();
        this.CustomGrouping(new LineReadModelGrouper());
    }

    public override async Task EnrichEventsAsync(SliceGroup<LineReadModel, Guid> group, IQuerySession querySession, CancellationToken cancellation)
    {
        foreach (var slice in group.Slices)
        {
            var tripAssignedEvents = slice.Events().OfType<IEvent<LineTripAssigned>>().ToArray();
            var segmentAppendedEvents = slice.Events().OfType<IEvent<LineSegmentAppended>>().ToArray();

            var tripIds = tripAssignedEvents.Select(e => e.Data.TripId).ToArray();
            var trips = await querySession.LoadManyAsync<LineTripReference>(cancellation, tripIds);
            var tripsById = trips.ToDictionary(lineTripReference => lineTripReference.Id, x => x);

            var segmentIds = segmentAppendedEvents.Select(e => e.Data.SegmentId).ToArray();
            var segments = await querySession.LoadManyAsync<LineSegmentReference>(cancellation, segmentIds);
            var segmentsById = segments.ToDictionary(x => x.Id, x => x);

            foreach (var tripAssignedEvent in tripAssignedEvents)
            {
                var reference = tripsById[tripAssignedEvent.Data.TripId];
                var trip = new LineReadModelTrip(reference.Id, reference.Number);
                var enriched = new LineTripAssignedWithReferences(tripAssignedEvent.Data, trip);

                slice.ReplaceEvent(tripAssignedEvent, enriched);
            }

            foreach (var segmentAppendedEvent in segmentAppendedEvents)
            {
                var reference = segmentsById[segmentAppendedEvent.Data.SegmentId];
                var a = new LineReadModelStation(reference.AId, null, "???");
                var b = new LineReadModelStation(reference.BId, null, "???");
                var segment = new LineReadModelSegment(reference.Id, a, b, reference.Course);
                var enriched = new LineSegmentAppendedWithReferences(segmentAppendedEvent.Data, segment);

                slice.ReplaceEvent(segmentAppendedEvent, enriched);
            }
        }
    }

    public void Apply(IEvent<LineCreated> e, LineReadModel readModel)
    {
        readModel.Id = e.Data.LineId;
        readModel.LineNumber = e.Data.LineNumber;
        readModel.LineLetter = e.Data.LineLetter;
        readModel.LineDesignation = $"{e.Data.LineNumber}{e.Data.LineLetter}";
        readModel.Segments = [];
        readModel.Stations = [];
        readModel.Trips = [];
    }

    public void Apply(IEvent<LineTripAssignedWithReferences> e, LineReadModel readModel)
    {
        readModel.Trips = readModel.Trips.Append(e.Data.Trip).ToList();
    }

    public void Apply(IEvent<LineSegmentAppendedWithReferences> e, LineReadModel readModel)
    {
        readModel.Segments = readModel.Segments.Append(e.Data.Segment).ToList();

        if (readModel.Stations.IsEmpty())
        {
            readModel.Stations = readModel.Stations.Append(e.Data.Segment.A).Append(e.Data.Segment.B).ToList();
        }
        else if (e.Data.Segment.A.Id == readModel.Stations.Last().Id)
        {
            readModel.Stations = readModel.Stations.Append(e.Data.Segment.B).ToList();
        }
        else if (e.Data.Segment.B.Id == readModel.Stations.Last().Id)
        {
            readModel.Stations = readModel.Stations.Append(e.Data.Segment.A).ToList();
        }
        else
        {
            throw new NotImplementedException("This will be handled once we fully figure out segment appening in all cases.");
        }
    }
}