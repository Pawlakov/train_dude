// <copyright file="SeedService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using TrainDude.Commands.Handlers.Seed;
using TrainDude.Commands.Requests.Admin;
using TrainDude.Commands.Requests.Stations;
using TrainDude.Commands.Requests.Trips;
using TrainDude.Domain.Events.Stations;
using TrainDude.Integration.Values;
using TrainDude.Web.Client.Seed;

public class SeedService
{
    private readonly Dictionary<int, Guid> stationIdMap;
    private readonly Dictionary<int, Guid> tripIdMap;

    private readonly HttpCommandSender mediator;
    private readonly SeedLoader loader;

    public SeedService(HttpCommandSender mediator, SeedLoader loader)
    {
        this.stationIdMap = new Dictionary<int, Guid>();
        this.tripIdMap = new Dictionary<int, Guid>();

        this.mediator = mediator;
        this.loader = loader;
    }

    public async Task Handle(CancellationToken cancellationToken = default)
    {
        await this.mediator.Send(new DropCommand(), cancellationToken);

        var stationsSeed = await this.loader.LoadAsync<StationSeed>("stations_seed.yml", cancellationToken);
        foreach (var stationSeed in stationsSeed)
        {
            await this.SeedStation(stationSeed, cancellationToken);
        }

        var tripsSeed = await this.loader.LoadAsync<TripSeed>("trips_seed.yml", cancellationToken);
        foreach (var tripSeed in tripsSeed)
        {
            await this.SeedTrip(tripSeed, cancellationToken);
        }

        /*var linesSeed = this.loader.Load<LineSeed>("lines_seed.yml");
        foreach (var lineSeed in linesSeed)
        {
            await this.SeedLine(lineSeed, cancellationToken);
        }

        var segmentsSeed = this.loader.Load<SegmentSeed>("segments_seed.yml");
        foreach (var segmentSeed in segmentsSeed)
        {
            await this.SeedSegment(segmentSeed, cancellationToken);
        }

        var radiiSeed = this.loader.Load<RadiusSeed>("radii_seed.yml");
        foreach (var radiusSeed in radiiSeed)
        {
            await this.SeedRadius(radiusSeed, cancellationToken);
        }*/
    }

    /*private async Task SeedLine(LineSeed seed, CancellationToken cancellationToken = default)
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
    }*/

    private async Task SeedStation(StationSeed seed, CancellationToken cancellationToken = default)
    {
        var stationId = Guid.NewGuid();
        var createCommand = new CreateStationCommand
        {
            Id = stationId,
            NameGerman = seed.NameGerman,
            NameGermanNew = seed.NameGermanNew,
            NamePolish = seed.NamePolish,
            NameRussian = seed.NameRussian,
        };

        await this.mediator.Send(createCommand, cancellationToken);
        this.stationIdMap[seed.Id] = stationId;
        var version = 1L;

        if (seed is { Latitude: not null, Longitude: not null })
        {
            var location = new Location(seed.Longitude.Value, seed.Latitude.Value);
            var setLocationCommand = new SetLocationCommand
            {
                Id = stationId,
                Version = version++,
                Location = location,
            };

            await this.mediator.Send(setLocationCommand, cancellationToken);
        }

        for (var i = 0; i < seed.AxleCount; ++i)
        {
            var addAxleCommand = new AddAxleCommand
            {
                Id = stationId,
                Version = version++,
            };

            await this.mediator.Send(addAxleCommand, cancellationToken);
        }
    }

    /*private async Task SeedSegment(SegmentSeed seed, CancellationToken cancellationToken = default)
    {
        var segmentId = Guid.NewGuid();

        var created = Segment.Make(segmentId);

        this.session.Events.StartStream<Trip>(segmentId, created);
        await this.session.SaveChangesAsync(cancellationToken);
        await this.publisher.Publish(created, cancellationToken);

        // TODO przywrócić segmenty do dawnej chwały
        var stream = await this.session.Events.FetchForWriting<SegmentSeed>(segmentId, cancellationToken);
        segment.AddExtremes(idDictionary[seed.A.StationId], idDictionary[seed.B.StationId]);
        segment.AddVertices(seed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []);
    }*/

    private async Task SeedTrip(TripSeed seed, CancellationToken cancellationToken = default)
    {
        var tripId = Guid.NewGuid();
        var createCommand = new CreateTripCommand
        {
            Id = tripId,
            Number = seed.Number,
        };

        await this.mediator.Send(createCommand, cancellationToken);
        this.tripIdMap[seed.Number] = tripId;
    }
}