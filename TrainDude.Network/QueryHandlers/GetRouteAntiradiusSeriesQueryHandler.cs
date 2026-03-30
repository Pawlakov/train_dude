// <copyright file="GetRouteAntiradiusSeriesQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using MongoDB.Driver.GeoJsonObjectModel;

using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetRouteAntiradiusSeriesQueryHandler : IRequestHandler<GetRouteAntiradiusSeriesQuery, string>
{
    private readonly StationService stationService;
    private readonly RouteService routeService;

    public GetRouteAntiradiusSeriesQueryHandler(StationService stationService, RouteService routeService)
    {
        this.stationService = stationService;
        this.routeService = routeService;
    }

    public async Task<string> Handle(GetRouteAntiradiusSeriesQuery request, CancellationToken cancellationToken)
    {
        var route = await this.routeService.Get(request.Id);
        if (route != null)
        {
            var a = await this.stationService.Get(route.A.StationId);
            var b = await this.stationService.Get(route.B.StationId);

            var points = route.MidPoints.Select(x => x.Location.Coordinates).Prepend(a?.Location?.Coordinates).Append(b?.Location?.Coordinates).ToArray();
            var segments = points.Segments().ToArray();

            var totalHaversine = segments.Haversine();
            var sampleLength = totalHaversine / request.Resolution;

            var currentSegment = segments[0];
            var currentPoint = segments[0].A;
            var samplePoints = new List<GeoJson2DGeographicCoordinates> { segments[0].A };
            for (var i = 1; i < request.Resolution; ++i)
            {
                // todo
                // 1. find a point x kilometers away from a towards b
                // 2. if b is closer to a than that point continue on the next segment
            }

            throw new NotImplementedException();
        }
        else
        {
            return "[]";
        }
    }
}