// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Admin.CommandHandlers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using TrainDude.Admin.Commands;
using TrainDude.Admin.Services;
using TrainDude.Data.Models;

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
        var routesSeed = await this.seedService.GetRoutesSeed();
        var radiiSeed = await this.seedService.GetRadiiSeed();
        var trainsSeed = await this.seedService.GetTrainsSeed();

        var idDictionary = new Dictionary<int, int>();
        foreach (var stationSeed in stationsSeed)
        {
            var station = new Station
            {
                NameGerman = stationSeed.NameGerman,
                NameGermanNew = stationSeed.NameGermanNew,
                NamePolish = stationSeed.NamePolish,
                NamePolishOld = stationSeed.NamePolishOld,
                Location = new Coordinates { Latitude = stationSeed.Latitude, Longitude = stationSeed.Longitude },
            };

            await this.db.Set<Station>().AddAsync(station, cancellationToken);

            idDictionary[stationSeed.Id] = station.Id;
        }

        foreach (var routeSeed in routesSeed)
        {
            var route = new Route
            {
                Ends = new List<RouteExtreme>
                {
                    new RouteExtreme
                    {
                        StationId = idDictionary[routeSeed.A.StationId],
                        IsEnd = false,
                    },
                    new RouteExtreme
                    {
                        StationId = idDictionary[routeSeed.B.StationId],
                        IsEnd = true,
                    },
                },
                NominalLength = routeSeed.Length,
            };

            await this.db.Set<Route>().AddAsync(route, cancellationToken);
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
    }
}