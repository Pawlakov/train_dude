// <copyright file="SeedCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.CommandHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly RadiusService radiusService;

    public SeedCommandHandler(SeedService seedService, StationService stationService, RouteService routeService, RadiusService radiusService)
    {
        this.seedService = seedService;
        this.stationService = stationService;
        this.routeService = routeService;
        this.radiusService = radiusService;
    }

    public async Task Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        var stationsSeed = await this.seedService.GetStationsSeed();
        var routesSeed = await this.seedService.GetRoutesSeed();
        var radiiSeed = await this.seedService.GetRadiiSeed();

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
                AxleCount = stationSeed.AxleCount,
            };

            idDictionary[stationSeed.Id] = await this.stationService.Insert(station);
        }

        foreach (var routeSeed in routesSeed)
        {
            var route = new Route
            {
                Id = default,
                A = new Route.EndPoint
                {
                    StationId = idDictionary[routeSeed.A.StationId],
                    Axle = routeSeed.A.Axle,
                    Pole = routeSeed.A.Pole,
                },
                B = new Route.EndPoint
                {
                    StationId = idDictionary[routeSeed.B.StationId],
                    Axle = routeSeed.B.Axle,
                    Pole = routeSeed.B.Pole,
                },
                NominalLength = routeSeed.Length,
                Tracks = routeSeed.Tracks,
                MidPoints = routeSeed.MidPoints?
                    .Select(x => new Route.MidPoint
                    {
                        Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(x.Longitude, x.Latitude)),
                    })?
                    .ToList() ?? new List<Route.MidPoint>(),
            };

            await this.routeService.Insert(route);
        }

        foreach (var radiusSeed in radiiSeed)
        {
            var radius = new Radius
            {
                Id = default,
                Speed = radiusSeed.Speed,
                Minimum = radiusSeed.Minimum,
            };

            await this.radiusService.Insert(radius);
        }
    }
}