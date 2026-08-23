// <copyright file="SeedService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Contracts.Admin;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Contracts.Segments;
using TrainDude.Commands.Contracts.Stations;
using TrainDude.Shared.Values;
using TrainDude.Web.Client.Seed;

using CreateCommand=TrainDude.Commands.Contracts.Radii.CreateCommand;

public class SeedService
{
    private readonly ConcurrentDictionary<int, Guid> stationIdMap;
    private readonly ConcurrentDictionary<int, Guid> tripIdMap;

    private readonly HttpCommandSender mediator;
    private readonly SeedLoader loader;

    public SeedService(HttpCommandSender mediator, SeedLoader loader)
    {
        this.stationIdMap = new ConcurrentDictionary<int, Guid>();
        this.tripIdMap = new ConcurrentDictionary<int, Guid>();

        this.mediator = mediator;
        this.loader = loader;
    }

    public async Task Handle(CancellationToken cancellationToken = default)
    {
        await this.mediator.Send(new DropCommand(), cancellationToken);

        var options = new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = 4,
        };

        var stationsSeedTask = this.loader.LoadAsync<StationSeed>("stations_seed.yml", cancellationToken);
        var radiiSeedTask = this.loader.LoadAsync<RadiusSeed>("radii_seed.yml", cancellationToken);
        var tripsSeedTask = this.loader.LoadAsync<TripSeed>("trips_seed.yml", cancellationToken);
        var segmentsSeedTask = this.loader.LoadAsync<SegmentSeed>("segments_seed.yml", cancellationToken);
        var linesSeedTask = this.loader.LoadAsync<LineSeed>("lines_seed.yml", cancellationToken);

        var stationsSeed = await stationsSeedTask;
        var stationsTask = Parallel.ForEachAsync(stationsSeed, options, async (x, ct) => await this.SeedStation(x, ct));
        var radiiSeed = await radiiSeedTask;
        var radiiTask = Parallel.ForEachAsync(radiiSeed, options, async (x, ct) => await this.SeedRadius(x, ct));
        var tripsSeed = await tripsSeedTask;
        var tripsTask = Parallel.ForEachAsync(tripsSeed, options, async (x, ct) => await this.SeedTrip(x, ct));
        var segmentsSeed = await segmentsSeedTask;
        await stationsTask;
        var segmentsTask = Parallel.ForEachAsync(segmentsSeed, options, async (x, ct) => await this.SeedSegment(x, ct));
        var linesSeed = await linesSeedTask;
        await tripsTask;
        var linesTask = Parallel.ForEachAsync(linesSeed, options, async (x, ct) => await this.SeedLine(x, ct));

        await Task.WhenAll(radiiTask, segmentsTask, linesTask);
    }

    private async Task SeedLine(LineSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new Commands.Contracts.Lines.CreateCommand
        {
            Id = Guid.NewGuid(),
            Number = seed.Number,
            Letter = seed.Letter,
        };

        var createdResponse = await this.mediator.Send(createCommand, cancellationToken);
        var version = 1L;

        foreach (var trip in seed.Trips)
        {
            var assignTripCommand = new AssignTripCommand
            {
                Id = createdResponse.Id,
                Version = version,
                TripId = this.tripIdMap[trip],
            };

            var updatedResult = await this.mediator.Send(assignTripCommand, cancellationToken);
            version = updatedResult.Version;
        }

        foreach (var station in seed.Stations)
        {
            var appendStationCommand = new AppendStationCommand
            {
                Id = createdResponse.Id,
                Version = version,
                StationId = this.stationIdMap[station],
            };

            var updatedResult = await this.mediator.Send(appendStationCommand, cancellationToken);
            version = updatedResult.Version;
        }
    }

    private async Task SeedRadius(RadiusSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new CreateCommand
        {
            Id = Guid.NewGuid(),
            Speed = seed.Speed,
            Minimum = seed.Minimum,
        };

        await this.mediator.Send(createCommand, cancellationToken);
    }

    private async Task SeedStation(StationSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new Commands.Contracts.Stations.CreateCommand
        {
            Id = Guid.NewGuid(),
            NameGerman = seed.NameGerman,
            NameGermanNew = seed.NameGermanNew,
            NamePolish = seed.NamePolish,
            NameRussian = seed.NameRussian,
        };

        var createdResponse = await this.mediator.Send(createCommand, cancellationToken);
        this.stationIdMap[seed.Id] = createdResponse.Id;
        var version = 1L;

        if (seed is { Latitude: not null, Longitude: not null })
        {
            var location = new Location(seed.Longitude.Value, seed.Latitude.Value);
            var setLocationCommand = new SetLocationCommand
            {
                Id = createdResponse.Id,
                Version = version,
                Location = location,
            };

            var updatedResponse = await this.mediator.Send(setLocationCommand, cancellationToken);
            version = updatedResponse.Version;
        }

        for (var i = 0; i < seed.AxleCount; ++i)
        {
            var addAxleCommand = new AddAxleCommand
            {
                Id = createdResponse.Id,
                Version = version,
            };

            var updatedResponse = await this.mediator.Send(addAxleCommand, cancellationToken);
            version = updatedResponse.Version;
        }
    }

    private async Task SeedSegment(SegmentSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new Commands.Contracts.Segments.CreateCommand
        {
            Id = Guid.NewGuid(),
            NominalLength = seed.Length,
            Tracks = seed.Tracks,
            AId = this.stationIdMap[seed.A.StationId],
            BId = this.stationIdMap[seed.B.StationId],
        };

        var createdResponse = await this.mediator.Send(createCommand, cancellationToken);
        var version = 1L;

        if (seed.Vertices is not null && seed.Vertices is not [])
        {
            var locations = (seed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []).ToList();
            var setCourseCommand = new SetCourseCommand
            {
                Id = createdResponse.Id,
                Version = version,
                Course = locations,
            };

            var updatedResponse = await this.mediator.Send(setCourseCommand, cancellationToken);
            version = updatedResponse.Version;
        }
    }

    private async Task SeedTrip(TripSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new Commands.Contracts.Trips.CreateCommand
        {
            Id = Guid.NewGuid(),
            Number = seed.Number,
        };

        var createdResponse = await this.mediator.Send(createCommand, cancellationToken);
        this.tripIdMap[seed.Number] = createdResponse.Id;
    }
}