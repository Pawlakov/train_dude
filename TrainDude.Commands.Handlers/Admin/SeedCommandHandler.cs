// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Mediator;

using TrainDude.Commands.Data.Documents;
using TrainDude.Commands.Handlers.Seed;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Lines;
using TrainDude.Shared.Notifications.Stations;
using TrainDude.Shared.Notifications.Trips;
using TrainDude.Shared.Values;

public sealed class SeedCommandHandler
    : ICommandHandler<SeedCommand>
{
    private readonly IDocumentStore store;
    private readonly IDocumentSession session;
    private readonly IPublisher publisher;

    public SeedCommandHandler(IDocumentStore store, IDocumentSession session, IPublisher publisher)
    {
        this.store = store;
        this.session = session;
        this.publisher = publisher;
    }

    public async ValueTask<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        await this.store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var linesSeed = SeedLoader.Load<LineSeed>("lines_seed.yml");
        var stationsSeed = SeedLoader.Load<StationSeed>("stations_seed.yml");
        var segmentsSeed = SeedLoader.Load<SegmentSeed>("segments_seed.yml");
        var radiiSeed = SeedLoader.Load<RadiusSeed>("radii_seed.yml");
        var tripsSeed = SeedLoader.Load<TripSeed>("trips_seed.yml");

        var stationIdMap = new Dictionary<int, Guid>();
        foreach (var stationSeed in stationsSeed)
        {
            var stationId = Guid.NewGuid();
            stationIdMap[stationSeed.Id] = stationId;

            var station = Station.Create(stationId, stationSeed.NameGerman, stationSeed.NameGermanNew, stationSeed.NamePolish, stationSeed.NameRussian);
            if (stationSeed is { Latitude: not null, Longitude: not null })
            {
                var location = new Location(stationSeed.Longitude.Value, stationSeed.Latitude.Value);
                station.SetLocation(location);
            }

            this.session.Events.StartStream<Station>(stationId, station.UncommittedEvents);
            await this.session.SaveChangesAsync(cancellationToken);
            foreach (var notification in station.UncommittedEvents)
            {
                await this.publisher.Publish(notification, cancellationToken);
            }

            station.ClearUncommittedEvents();
        }

        var tripIdMap = new Dictionary<int, Guid>();
        foreach (var tripSeed in tripsSeed)
        {
            var tripId = Guid.NewGuid();
            tripIdMap[tripSeed.Number] = tripId;

            var trip = Trip.Create(tripId, tripSeed.Number);

            this.session.Events.StartStream<Trip>(tripId, trip.UncommittedEvents);
            await this.session.SaveChangesAsync(cancellationToken);
            foreach (var notification in trip.UncommittedEvents)
            {
                await this.publisher.Publish(notification, cancellationToken);
            }

            trip.ClearUncommittedEvents();
        }

        foreach (var lineSeed in linesSeed)
        {
            var lineId = Guid.NewGuid();

            var line = Line.Create(lineId, lineSeed.Number, lineSeed.Letter);
            foreach (var trip in lineSeed.Trips)
            {
                this.session.Events.Append(lineId, new LineTripAssignedNotification(lineId, tripIdMap[trip]));
            }

            foreach (var station in lineSeed.Stations)
            {
                this.session.Events.Append(lineId, new LineStationAppendedNotification(lineId, stationIdMap[station]));
            }

            this.session.Events.StartStream<Line>(lineId, line.UncommittedEvents);
            await this.session.SaveChangesAsync(cancellationToken);
            foreach (var notification in line.UncommittedEvents)
            {
                await this.publisher.Publish(notification, cancellationToken);
            }

            line.ClearUncommittedEvents();
        }

        /*foreach (var segmentSeed in segmentsSeed)
        {
            var segment = new Segment(segmentSeed.Length);
            segment.AddExtremes(idDictionary[segmentSeed.A.StationId], idDictionary[segmentSeed.B.StationId]);
            segment.AddVertices(segmentSeed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []);

            await this.db.Segments.AddAsync(segment, cancellationToken);
        }*/

        foreach (var radiusSeed in radiiSeed)
        {
            var radiusId = Guid.NewGuid();

            var radius = Radius.Create(radiusId, radiusSeed.Speed, radiusSeed.Minimum);

            this.session.Events.StartStream<Radius>(radiusId, radius.UncommittedEvents);
            await this.session.SaveChangesAsync(cancellationToken);
            foreach (var notification in radius.UncommittedEvents)
            {
                await this.publisher.Publish(notification, cancellationToken);
            }

            radius.ClearUncommittedEvents();
        }

        return Unit.Value;
    }
}