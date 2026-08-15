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

using TrainDude.Commands.Handlers.Seed;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Domain.Documents;
using TrainDude.Shared.Values;

public sealed class SeedCommandHandler
    : ICommandHandler<SeedCommand>
{
    private readonly IDocumentStore store;
    private readonly IDocumentSession session;
    private readonly IPublisher publisher;

    private readonly Dictionary<int, Guid> stationIdMap;
    private readonly Dictionary<int, Guid> tripIdMap;

    public SeedCommandHandler(IDocumentStore store, IDocumentSession session, IPublisher publisher)
    {
        this.store = store;
        this.session = session;
        this.publisher = publisher;

        this.stationIdMap = new Dictionary<int, Guid>();
        this.tripIdMap = new Dictionary<int, Guid>();
    }

    public async ValueTask<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        await this.store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var linesSeed = SeedLoader.Load<LineSeed>("lines_seed.yml");
        var stationsSeed = SeedLoader.Load<StationSeed>("stations_seed.yml");
        var segmentsSeed = SeedLoader.Load<SegmentSeed>("segments_seed.yml");
        var radiiSeed = SeedLoader.Load<RadiusSeed>("radii_seed.yml");
        var tripsSeed = SeedLoader.Load<TripSeed>("trips_seed.yml");

        foreach (var stationSeed in stationsSeed)
        {
            await this.SeedStation(stationSeed, cancellationToken);
        }

        foreach (var tripSeed in tripsSeed)
        {
            await this.SeedTrip(tripSeed, cancellationToken);
        }

        foreach (var lineSeed in linesSeed)
        {
            await this.SeedLine(lineSeed, cancellationToken);
        }

        foreach (var segmentSeed in segmentsSeed)
        {
            await this.SeedSegment(segmentSeed, cancellationToken);
        }

        foreach (var radiusSeed in radiiSeed)
        {
            await this.SeedRadius(radiusSeed, cancellationToken);
        }

        return Unit.Value;
    }

    private async Task SeedLine(LineSeed seed, CancellationToken cancellationToken = default)
    {
        var lineId = Guid.NewGuid();

        var created = Line.Make(lineId, seed.Number, seed.Letter);

        this.session.Events.StartStream<Line>(lineId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);

        var stream = await this.session.Events.FetchForWriting<Line>(lineId, cancellationToken);
        foreach (var trip in seed.Trips)
        {
            var tripAssigned = stream.Aggregate.AssignTrip(this.tripIdMap[trip]);

            stream.AppendOne(tripAssigned);
            await this.session.SaveChangesAsync(cancellationToken);
            await this.publisher.Publish(tripAssigned, cancellationToken);
        }

        foreach (var station in seed.Stations)
        {
            var stationAppended = stream.Aggregate.AppendStation(this.stationIdMap[station]);

            stream.AppendOne(stationAppended);
            await this.session.SaveChangesAsync(cancellationToken);
            await this.publisher.Publish(stationAppended, cancellationToken);
        }
    }

    private async Task SeedRadius(RadiusSeed seed, CancellationToken cancellationToken = default)
    {
        var radiusId = Guid.NewGuid();

        var created = Radius.Make(radiusId, seed.Speed, seed.Minimum);

        this.session.Events.StartStream<Radius>(radiusId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);
    }

    private async Task SeedStation(StationSeed seed, CancellationToken cancellationToken = default)
    {
        var stationId = Guid.NewGuid();
        this.stationIdMap[seed.Id] = stationId;

        var created = Station.Make(stationId, seed.NameGerman, seed.NameGermanNew, seed.NamePolish, seed.NameRussian);

        this.session.Events.StartStream<Station>(stationId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);

        var stream = await this.session.Events.FetchForWriting<Station>(stationId, cancellationToken);
        if (seed is { Latitude: not null, Longitude: not null })
        {
            var location = new Location(seed.Longitude.Value, seed.Latitude.Value);
            var locationSet = stream.Aggregate.SetLocation(location);

            stream.AppendOne(locationSet);
            await this.session.SaveChangesAsync(cancellationToken);
            await this.publisher.Publish(locationSet, cancellationToken);
        }

        for (var i = 0; i < seed.AxleCount; ++i)
        {
            stream.Aggregate.AddAxle();
        }
    }

    private async Task SeedSegment(SegmentSeed seed, CancellationToken cancellationToken = default)
    {
        var segmentId = Guid.NewGuid();

        var created = Segment.Make(segmentId);

        this.session.Events.StartStream<Trip>(segmentId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);

        // TODO przywrócić segmenty do dawnej chwały
        /*var stream = await this.session.Events.FetchForWriting<SegmentSeed>(segmentId, cancellationToken);
        segment.AddExtremes(idDictionary[seed.A.StationId], idDictionary[seed.B.StationId]);
        segment.AddVertices(seed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []);*/
    }

    private async Task SeedTrip(TripSeed seed, CancellationToken cancellationToken = default)
    {
        var tripId = Guid.NewGuid();
        this.tripIdMap[seed.Number] = tripId;

        var created = Trip.Make(tripId, seed.Number);

        this.session.Events.StartStream<Trip>(tripId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);
    }
}