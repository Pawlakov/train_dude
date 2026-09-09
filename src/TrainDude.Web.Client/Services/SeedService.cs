// <copyright file="SeedService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Features.Lines.Contracts.AppendSegment;
using TrainDude.Features.Lines.Contracts.AssignTrip;
using TrainDude.Features.Lines.Contracts.CreateLine;
using TrainDude.Features.Radii.Contracts.CreateRadius;
using TrainDude.Features.Segments.Contracts.CreateSegment;
using TrainDude.Features.Segments.Contracts.SetCourse;
using TrainDude.Features.Shared.Contracts.Admin;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.AddAxle;
using TrainDude.Features.Stations.Contracts.CreateStation;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Features.Trips.Contracts.CreateTrip;
using TrainDude.Web.Client.Seed;

public class SeedService
{
    private readonly ConcurrentDictionary<int, Guid> segmentIdMap;
    private readonly ConcurrentDictionary<int, Guid> stationIdMap;
    private readonly ConcurrentDictionary<int, Guid> tripIdMap;

    private readonly ApiClient api;
    private readonly SeedLoader loader;

    public SeedService(ApiClient api, SeedLoader loader)
    {
        this.segmentIdMap = new ConcurrentDictionary<int, Guid>();
        this.stationIdMap = new ConcurrentDictionary<int, Guid>();
        this.tripIdMap = new ConcurrentDictionary<int, Guid>();

        this.api = api;
        this.loader = loader;
    }

    public async Task Handle(CancellationToken cancellationToken = default)
    {
        await this.api.SendAsync<DropCommand, EmptyResult>(new DropCommand(), cancellationToken);

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
        var createCommand = new CreateLineCommand(seed.Number, seed.Letter);

        var createdResponse = await this.api.SendAsync<CreateLineCommand, CreatedResult>(createCommand, cancellationToken);
        var version = 1L;

        foreach (var segment in seed.Segments)
        {
            var appendSegmentCommand = new AppendSegmentCommand(createdResponse.Id, version, this.segmentIdMap[segment]);

            version = (await this.api.SendAsync<AppendSegmentCommand, UpdatedResult>(appendSegmentCommand, cancellationToken)).Version;
        }

        foreach (var trip in seed.Trips)
        {
            var assignTripCommand = new AssignTripCommand(createdResponse.Id, version, this.tripIdMap[trip]);

            version = (await this.api.SendAsync<AssignTripCommand, UpdatedResult>(assignTripCommand, cancellationToken)).Version;
        }
    }

    private async Task SeedRadius(RadiusSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new CreateRadiusCommand(seed.Speed, seed.Minimum);

        await this.api.SendAsync<CreateRadiusCommand, CreatedResult>(createCommand, cancellationToken);
    }

    private async Task SeedStation(StationSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new CreateStationCommand(seed.NameGerman, seed.NameGermanNew, seed.NamePolish, seed.NameRussian);

        var createdResponse = await this.api.SendAsync<CreateStationCommand, CreatedResult>(createCommand, cancellationToken);
        this.stationIdMap[seed.Id] = createdResponse.Id;
        var version = 1L;

        if (seed is { Latitude: not null, Longitude: not null })
        {
            var location = new Location(seed.Longitude.Value, seed.Latitude.Value);
            var setLocationCommand = new SetLocationCommand(createdResponse.Id, version, location);

            var updatedResponse = await this.api.SendAsync<SetLocationCommand, UpdatedResult>(setLocationCommand, cancellationToken);
            version = updatedResponse.Version;
        }

        var axleCount = seed.AxleCount ?? 1;
        for (var i = 0; i < axleCount; ++i)
        {
            var addAxleCommand = new AddAxleCommand(createdResponse.Id, version);

            var updatedResponse = await this.api.SendAsync<AddAxleCommand, UpdatedResult>(addAxleCommand, cancellationToken);
            version = updatedResponse.Version;
        }
    }

    private async Task SeedSegment(SegmentSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new CreateSegmentCommand(seed.Length, seed.Tracks, this.stationIdMap[seed.A.StationId], seed.A.Axle ?? 0, seed.A.Pole, this.stationIdMap[seed.B.StationId], seed.B.Axle ?? 0, seed.B.Pole);

        var segmentId = (await this.api.SendAsync<CreateSegmentCommand, CreatedResult>(createCommand, cancellationToken)).Id;
        this.segmentIdMap[seed.Id] = segmentId;

        var version = 1L;
        if (seed.Course is not null && seed.Course is not [])
        {
            var locations = (seed.Course?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []).ToList();
            var setCourseCommand = new SetCourseCommand(segmentId, version, locations);

            var updatedResponse = await this.api.SendAsync<SetCourseCommand, UpdatedResult>(setCourseCommand, cancellationToken);
            version = updatedResponse.Version;
        }
    }

    private async Task SeedTrip(TripSeed seed, CancellationToken cancellationToken = default)
    {
        var createCommand = new CreateTripCommand(seed.Number);

        var createdResponse = await this.api.SendAsync<CreateTripCommand, CreatedResult>(createCommand, cancellationToken);
        this.tripIdMap[seed.Number] = createdResponse.Id;
    }
}