// <copyright file="AppendSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Domain;
using TrainDude.Domain.Lines;
using TrainDude.Domain.Segments;
using TrainDude.Domain.Stations;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendSegmentEndpoint
{
    [AggregateHandler]
    [WolverinePost(AppendSegmentCommand.Route)]
    public static async Task<(UpdatedResponse, Events, OutgoingMessages)> Post(
        AppendSegmentCommand command,
        [WriteModel(Required = true)] LineAggregate aggregate,
        [ReadModel(nameof(AppendSegmentCommand.SegmentId))] SegmentAggregate segmentAggregate,
        IDocumentSession session,
        CancellationToken cancellationToken = default)
    {
        var events = new Events();

        var allSegments = await session.Query<SegmentAggregate>()
            .Where(x => aggregate.Segments.Contains(x.Id))
            .ToListAsync(cancellationToken);

        // TODO actual validation that doesnt assume we always append on the right end (or a consecutive segment at all)
        /*if (aggregate.Segments.Count > 0)
        {
            var lastSegment = await session.Events.FetchLatest<SegmentAggregate>(aggregate.Segments[], cancellationToken);
            if (aggregate.Segments.Count > 1)
            {
                var firstSegment = await session.Events.FetchLatest<SegmentAggregate>(aggregate.Segments[0], cancellationToken);
                var secondSegment = await session.Events.FetchLatest<SegmentAggregate>(aggregate.Segments[1], cancellationToken);
                var secondToLastSegment = await session.Events.FetchLatest<SegmentAggregate>(aggregate.Segments[], cancellationToken);
            }
            else
            {
                var flipEvent = () switch
                {

                };
            }

            // TODO validate and if needed flip
            throw new NotImplementedException();

            var flipEvent = aggregate.FlipOrder();
            events.Add(flipEvent);
            aggregate.Apply(flipEvent);
        }*/

        var appendEvent = aggregate.AppendSegment(segmentAggregate.Id);
        events.Add(appendEvent);
        aggregate.Apply(appendEvent);

        var allStationIds = allSegments
            .Append(segmentAggregate)
            .SelectMany(x => new[] { x.A.Id, x.B.Id })
            .GroupBy(x => x)
            .Select(x => x.Key)
            .ToList();
        var allStations = await session.Query<StationAggregate>()
            .Where(x => allStationIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var nameMode = await SettingsAccessor.GetNameMode(session, cancellationToken);
        var nameSelector = StationNameResolver.GetNameSelector(nameMode);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new LineSegmentAppendedIntegrationEvent(
        appendEvent.Id,
        aggregate.Version + 1,
        allStations.Select(x => new LineSegmentAppendedIntegrationEvent.Station(x.Id, nameSelector(x), x.Location)),
        aggregate.Segments.Select(x => new LineSegmentAppendedIntegrationEvent.Segment(x)));

        return (response, events, new OutgoingMessages { integrationEvent });
    }
}