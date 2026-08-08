// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.SeedCommand;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Mediator;

using TrainDude.Commands.Data.Documents;
using TrainDude.Commands.Data.Events;
using TrainDude.Commands.Handlers.Seed;
using TrainDude.Commands.Requests.SeedCommand;
using TrainDude.Shared.Notifications;
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
        /*var segmentsSeed = SeedLoader.Load<SegmentSeed>("segments_seed.yml");
        var radiiSeed = SeedLoader.Load<RadiusSeed>("radii_seed.yml");*/
        var tripsSeed = SeedLoader.Load<TripSeed>("trips_seed.yml");

        var stationIdMap = new Dictionary<int, Guid>();
        foreach (var stationSeed in stationsSeed)
        {
            var stationId = Guid.NewGuid();
            this.session.Events.StartStream<Station>(stationId, new StationCreated(stationId, stationSeed.NameGerman));
            stationIdMap[stationSeed.Id] = stationId;

            if (stationSeed is { Latitude: not null, Longitude: not null })
            {
                var location = new Location(stationSeed.Longitude.Value, stationSeed.Latitude.Value);
                this.session.Events.Append(stationId, new StationLocationSet(location));
            }
        }

        var tripIdMap = new Dictionary<int, Guid>();
        foreach (var tripSeed in tripsSeed)
        {
            var tripId = Guid.NewGuid();
            this.session.Events.StartStream<Trip>(tripId, new TripCreated(tripId, tripSeed.Number));
            tripIdMap[tripSeed.Number] = tripId;
        }

        foreach (var lineSeed in linesSeed)
        {
            var lineId = Guid.NewGuid();
            this.session.Events.StartStream<Line>(lineId, new LineCreated(lineId, lineSeed.Number, lineSeed.Letter));

            foreach (var trip in lineSeed.Trips)
            {
                this.session.Events.Append(lineId, new LineTripAssigned(tripIdMap[trip]));
            }

            foreach (var station in lineSeed.Stations)
            {
                this.session.Events.Append(lineId, new LineStationAppended(stationIdMap[station]));
            }
        }

        /*foreach (var segmentSeed in segmentsSeed)
        {
            var segment = new Segment(segmentSeed.Length);
            segment.AddExtremes(idDictionary[segmentSeed.A.StationId], idDictionary[segmentSeed.B.StationId]);
            segment.AddVertices(segmentSeed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []);

            await this.db.Segments.AddAsync(segment, cancellationToken);
        }

        foreach (var radiusSeed in radiiSeed)
        {
            var radius = new Radius(radiusSeed.Speed, radiusSeed.Minimum);

            await this.db.Radii.AddAsync(radius, cancellationToken);
        }*/
        await this.session.SaveChangesAsync(cancellationToken);

        await this.publisher.Publish(new DataChangedNotification(), cancellationToken);

        return Unit.Value;
    }
}