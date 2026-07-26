// <copyright file="GetRouteGeoJsonQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.Queries;

internal class GetRouteGeoJsonQueryHandler : IRequestHandler<GetSegmentGeoJsonQuery, string>
{
    private readonly NetworkDbContext db;

    public GetRouteGeoJsonQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<string> Handle(GetSegmentGeoJsonQuery request, CancellationToken cancellationToken)
    {
        var routesGeoJson = new List<string>();
        var aLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && !x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        var bLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        if (aLocation != null && bLocation != null)
        {
            var stationsGeoJson = new List<string>();
            foreach (var location in new[] { aLocation, bLocation })
            {
                stationsGeoJson.Add($"{{ type: \"Point\", coordinates: [ {location} ] }}");
            }

            var points = new[] { aLocation, bLocation }
                .Where(x => x != null)
                .Cast<StationLocation>()
                .ToArray();

            var line = string.Join(',', points.Select(x => $"[{x.ToString()}]"));

            routesGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");

            return $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]";
        }
        else
        {
            return "[]";
        }
    }
}