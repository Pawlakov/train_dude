// <copyright file="DropAndSeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.DropAndSeedCommand;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Commands.Data;
using TrainDude.Commands.Data.Entities;
using TrainDude.Commands.Handlers.Seed;
using TrainDude.Commands.Requests.DropAndSeedCommand;
using TrainDude.Shared.Values;

public sealed class DropAndSeedCommandHandler
    : ICommandHandler<DropAndSeedCommand>
{
    private readonly IWriteDbContext db;

    public DropAndSeedCommandHandler(IWriteDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<Unit> Handle(DropAndSeedCommand request, CancellationToken cancellationToken)
    {
        /* DROP */
        await this.db.Trips.ExecuteDeleteAsync(cancellationToken);
        await this.db.Lines.ExecuteDeleteAsync(cancellationToken);
        await this.db.Segments.ExecuteDeleteAsync(cancellationToken);
        await this.db.Stations.ExecuteDeleteAsync(cancellationToken);

        /* SEED */

        await this.db.Radii.ExecuteDeleteAsync(cancellationToken);

        await this.db.SaveChangesAsync(cancellationToken);

        var linesSeed = SeedLoader.Load<LineSeed>("lines_seed.yml");
        var stationsSeed = SeedLoader.Load<StationSeed>("stations_seed.yml");
        var segmentsSeed = SeedLoader.Load<SegmentSeed>("segments_seed.yml");
        var radiiSeed = SeedLoader.Load<RadiusSeed>("radii_seed.yml");
        var tripsSeed = SeedLoader.Load<TripSeed>("trips_seed.yml");

        foreach (var lineSeed in linesSeed)
        {
            var line = new Line(lineSeed.Number, lineSeed.Letter);

            await this.db.Lines.AddAsync(line, cancellationToken);
        }

        var idDictionary = new Dictionary<int, Station>();
        foreach (var stationSeed in stationsSeed)
        {
            var location = stationSeed is { Latitude: not null, Longitude: not null } ? new Location(stationSeed.Longitude.Value, stationSeed.Latitude.Value) : (Location?)null;
            var station = new Station(stationSeed.NameGerman, stationSeed.NameGermanNew, stationSeed.NamePolish, stationSeed.NamePolishOld, location);

            await this.db.Stations.AddAsync(station, cancellationToken);

            idDictionary[stationSeed.Id] = station;
        }

        foreach (var segmentSeed in segmentsSeed)
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
        }

        foreach (var tripSeed in tripsSeed)
        {
            var trip = new Trip(tripSeed.Number);

            await this.db.Trips.AddAsync(trip, cancellationToken);
        }

        await this.db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}