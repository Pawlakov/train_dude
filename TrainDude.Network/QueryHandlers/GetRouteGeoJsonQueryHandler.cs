// <copyright file="GetRouteGeoJsonQueryHandler.cs" company="Pawlakov">
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

internal class GetRouteGeoJsonQueryHandler : IRequestHandler<GetRouteGeoJsonQuery, string>
{
    private readonly StationService stationService;
    private readonly RouteService routeService;

    public GetRouteGeoJsonQueryHandler(StationService stationService, RouteService routeService)
    {
        this.stationService = stationService;
        this.routeService = routeService;
    }

    public async Task<string> Handle(GetRouteGeoJsonQuery request, CancellationToken cancellationToken)
    {
        var routesGeoJson = new List<string>();
        var route = await this.routeService.Get(request.Id);
        if (route != null)
        {
            var stationsGeoJson = new List<string>();
            var stationA = await this.stationService.Get(route.A.StationId);
            var stationB = await this.stationService.Get(route.B.StationId);
            foreach (var station in new[] { stationA, stationB })
            {
                if (station?.Location != null)
                {
                    stationsGeoJson.Add(station?.Location.ToJson()!);
                }
            }

            var points = route.MidPoints
                .Select(x => x.Location.Coordinates)
                .Prepend(stationA?.Location?.Coordinates)
                .Append(stationB?.Location?.Coordinates)
                .Where(x => x != null)
                .ToArray();

            var line = new GeoJsonLineString<GeoJson2DGeographicCoordinates>(new GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>(points));

            routesGeoJson.Add(line.ToJson());

            return $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]";
        }
        else
        {
            return "[]";
        }
    }
}