// <copyright file="GetNetworkGeoJsonQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetNetworkGeoJsonQueryHandler : IRequestHandler<GetNetworkGeoJsonQuery, string>
{
    private readonly StationService stationService;
    private readonly RouteService routeService;

    public GetNetworkGeoJsonQueryHandler(StationService stationService, RouteService routeService)
    {
        this.stationService = stationService;
        this.routeService = routeService;
    }

    public async Task<string> Handle(GetNetworkGeoJsonQuery request, CancellationToken cancellationToken)
    {
        var stationsGeoJson = new List<string>();
        var stations = await this.stationService.GetAll();
        var stationPoints = new Dictionary<ObjectId, GeoJsonPoint<GeoJson2DGeographicCoordinates>>();
        foreach (var station in stations)
        {
            var point = station.Location;
            if (point != null)
            {
                stationPoints.Add(station.Id, point);
            }

            stationsGeoJson.Add(point.ToJson());
        }

        var routesGeoJson = new List<string>();
        var routes = await this.routeService.GetAll();
        foreach (var route in routes)
        {
            var line = new GeoJsonLineString<GeoJson2DGeographicCoordinates>(new GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates[] { stationPoints[route.A].Coordinates, stationPoints[route.B].Coordinates }));

            routesGeoJson.Add(line.ToJson());
        }

        return $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]";
    }
}