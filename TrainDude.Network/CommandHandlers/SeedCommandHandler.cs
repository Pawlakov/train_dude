// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.CommandHandlers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using TrainDude.Network.Commands;
using TrainDude.Network.Models;
using TrainDude.Network.Services;

internal class SeedCommandHandler : IRequestHandler<SeedCommand>
{
    private readonly SeedService seedService;
    private readonly StationService stationService;
    private readonly RouteService routeService;

    public SeedCommandHandler(SeedService seedService, StationService stationService, RouteService routeService)
    {
        this.seedService = seedService;
        this.stationService = stationService;
        this.routeService = routeService;
    }

    public async Task Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        var stationsSeed = await this.seedService.GetStationsSeed();
        var routesSeed = await this.seedService.GetRoutesSeed();

        var idDictionary = new Dictionary<int, ObjectId>();
        foreach (var stationSeed in stationsSeed)
        {
            var station = new Station
            {
                Id = default,
                NameGerman = stationSeed.NameGerman,
                NameGermanNew = stationSeed.NameGermanNew,
                NamePolish = stationSeed.NamePolish,
                NamePolishOld = stationSeed.NamePolishOld,
                Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(stationSeed.Longitude, stationSeed.Latitude)),
                Altitude = stationSeed.Elevation,
            };

            idDictionary[stationSeed.Id] = await this.stationService.Insert(station);
        }

        foreach (var routeSeed in routesSeed)
        {
            var route = new Route
            {
                Id = default,
                A = idDictionary[routeSeed.A],
                B = idDictionary[routeSeed.B],
                NominalLength = routeSeed.Length,
                Tracks = routeSeed.Tracks,
            };

            await this.routeService.Insert(route);
        }
    }
}