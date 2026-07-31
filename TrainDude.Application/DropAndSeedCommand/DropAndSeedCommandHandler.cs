// <copyright file="DropAndSeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.DropAndSeedCommand;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.DropAndSeedCommand;
using TrainDude.Application.Seed;
using TrainDude.Data;
using TrainDude.Data.Entities;

internal class DropAndSeedCommandHandler
    : IRequestHandler<DropAndSeedCommand>
{
    private readonly NetworkDbContext db;

    public DropAndSeedCommandHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task Handle(DropAndSeedCommand request, CancellationToken cancellationToken)
    {
        await this.db.Radii.ExecuteDeleteAsync(cancellationToken);
        await this.db.Segments.ExecuteDeleteAsync(cancellationToken);
        await this.db.Stations.ExecuteDeleteAsync(cancellationToken);
        await this.db.Lines.ExecuteDeleteAsync(cancellationToken);

        await this.db.SaveChangesAsync(cancellationToken);

        var linesSeed = SeedLoader.Load<LineSeed>("lines_seed.yml");
        var stationsSeed = SeedLoader.Load<StationSeed>("stations_seed.yml");
        var routesSeed = SeedLoader.Load<SegmentSeed>("segments_seed.yml");
        var radiiSeed = SeedLoader.Load<RadiusSeed>("radii_seed.yml");
        var trainsSeed = SeedLoader.Load<TrainSeed>("trains_seed.yml");

        foreach (var lineSeed in linesSeed)
        {
            var line = new Line
            {
                LineId = lineSeed.Id,
            };

            await this.db.Lines.AddAsync(line, cancellationToken);
        }

        var idDictionary = new Dictionary<int, Station>();
        foreach (var stationSeed in stationsSeed)
        {
            var location = stationSeed.Latitude.HasValue && stationSeed.Longitude.HasValue ? new StationLocation { Latitude = stationSeed.Latitude.Value, Longitude = stationSeed.Longitude.Value } : null;
            var station = new Station
            {
                NameGerman = stationSeed.NameGerman,
                NameGermanNew = stationSeed.NameGermanNew,
                NamePolish = stationSeed.NamePolish,
                NamePolishOld = stationSeed.NamePolishOld,
                Location = location,
            };

            await this.db.Set<Station>().AddAsync(station, cancellationToken);

            idDictionary[stationSeed.Id] = station;
        }

        foreach (var routeSeed in routesSeed)
        {
            var vertices = (routeSeed.Vertices ?? [])
                .Select((x, index) => new SegmentVertexLocation { OrdinalId = index, Longitude = x.Longitude, Latitude = x.Latitude })
                .ToList();

            var route = new Segment
            {
                NominalLength = routeSeed.Length,
                Extremes = new List<SegmentExtreme>
                {
                    new SegmentExtreme
                    {
                        Station = idDictionary[routeSeed.A.StationId],
                        IsEnd = false,
                    },
                    new SegmentExtreme
                    {
                        Station = idDictionary[routeSeed.B.StationId],
                        IsEnd = true,
                    },
                },
                Vertices = vertices,
                Lines = routeSeed.Charts.Select(x => new LineSegment { LineId = x }).ToList(),
            };

            await this.db.Set<Segment>().AddAsync(route, cancellationToken);
        }

        foreach (var radiusSeed in radiiSeed)
        {
            var radius = new Radius
            {
                Speed = radiusSeed.Speed,
                Minimum = radiusSeed.Minimum,
            };

            await this.db.Set<Radius>().AddAsync(radius, cancellationToken);
        }

        await this.db.SaveChangesAsync(cancellationToken);
    }
}