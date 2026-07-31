// <copyright file="GetSegmentGeoJsonQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentGeoJsonQuery;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetSegmentGeoJsonQuery;
using TrainDude.Data;
using TrainDude.Data.Entities;

internal class GetSegmentGeoJsonQueryHandler
    : IRequestHandler<GetSegmentGeoJsonQuery, GetSegmentGeoJsonQueryResult>
{
    private readonly NetworkDbContext db;

    public GetSegmentGeoJsonQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<GetSegmentGeoJsonQueryResult> Handle(GetSegmentGeoJsonQuery request, CancellationToken cancellationToken)
    {
        var routesGeoJson = new List<string>();
        var aLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && !x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        var bLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        var vertices = await this.db.Set<SegmentVertexLocation>().Where(x => x.SegmentId == request.SegmentId).OrderBy(x => x.OrdinalId).ToListAsync(cancellationToken);
        if (aLocation != null && bLocation != null)
        {
            var stationsGeoJson = new List<string>();
            foreach (var location in new[] { aLocation, bLocation })
            {
                stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": {location} }}");
            }

            var points = vertices
                .Cast<Location>()
                .Prepend(aLocation)
                .Append(bLocation)
                .ToList();

            var line = string.Join(',', points);

            routesGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");

            return new GetSegmentGeoJsonQueryResult
            {
                GeoJson = $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]",
            };
        }
        else
        {
            return new GetSegmentGeoJsonQueryResult
            {
                GeoJson = "[]",
            };
        }
    }
}