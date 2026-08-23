// <copyright file="SeedService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Contracts.Admin;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Contracts.Stations;
using TrainDude.Shared.Values;
using TrainDude.Web.Client.Seed;

using CreateCommand=TrainDude.Commands.Contracts.Radii.CreateCommand;

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

        var radiiSeed = await this.loader.LoadAsync<RadiusSeed>("radii_seed.yml", cancellationToken);
        foreach (var radiusSeed in radiiSeed)
        {
            await this.SeedRadius(radiusSeed, cancellationToken);
        }

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

        var segmentsSeed = await this.loader.LoadAsync<SegmentSeed>("segments_seed.yml", cancellationToken);
        foreach (var segmentSeed in segmentsSeed)
        {
            await this.SeedSegment(segmentSeed, cancellationToken);
        }

        var linesSeed = await this.loader.LoadAsync<LineSeed>("lines_seed.yml", cancellationToken);
        foreach (var lineSeed in linesSeed)
        {
            await this.SeedLine(lineSeed, cancellationToken);
        }
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
            AId = this.stationIdMap[seed.A.StationId],
            BId = this.stationIdMap[seed.B.StationId],
        };

        await this.mediator.Send(createCommand, cancellationToken);

        /*segment.AddVertices(seed.Vertices?.Select(x => new Location(x.Longitude, x.Latitude)) ?? []);*/
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