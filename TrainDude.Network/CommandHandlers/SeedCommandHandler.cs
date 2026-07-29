// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.CommandHandlers;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TrainDude.Data;
using TrainDude.Data.Models;
using TrainDude.Network.Commands;
using TrainDude.Network.Services;

internal class SeedCommandHandler : IRequestHandler<SeedCommand>
{
    private readonly NetworkDbContext db;
    private readonly SeedService seedService;

    public SeedCommandHandler(SeedService seedService, NetworkDbContext db)
    {
        this.seedService = seedService;
        this.db = db;
    }

    public async Task Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        var stationsSeed = await this.seedService.GetStationsSeed();
        var routesSeed = await this.seedService.GetSegmentsSeed();
        var radiiSeed = await this.seedService.GetRadiiSeed();
        var trainsSeed = await this.seedService.GetTrainsSeed();

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

        var allCharts = routesSeed
            .SelectMany(x => x.Charts)
            .GroupBy(x => x)
            .Select(x => new Chart { ChartId = x.Key })
            .ToDictionary(x => x.ChartId, x => x);

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
                Charts = routeSeed.Charts.Select(x => new ChartSegment { Chart = allCharts[x] }).ToList(),
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