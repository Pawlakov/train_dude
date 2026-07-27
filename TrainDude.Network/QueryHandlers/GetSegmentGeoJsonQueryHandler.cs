// <copyright file="GetSegmentGeoJsonQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.Queries;

internal class GetSegmentGeoJsonQueryHandler : IRequestHandler<GetSegmentGeoJsonQuery, string>
{
    private readonly NetworkDbContext db;

    public GetSegmentGeoJsonQueryHandler(NetworkDbContext db)
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
                stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": {location} }}");
            }

            var points = new[] { aLocation, bLocation }
                .Where(x => x != null)
                .Cast<StationLocation>()
                .ToArray();

            var line = string.Join(',', points);

            routesGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");

            return $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]";
        }
        else
        {
            return "[]";
        }
    }
}