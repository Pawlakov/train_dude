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

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.Queries;

internal class GetNetworkGeoJsonQueryHandler : IRequestHandler<GetNetworkGeoJsonQuery, string>
{
    private readonly NetworkDbContext db;

    public GetNetworkGeoJsonQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<string> Handle(GetNetworkGeoJsonQuery request, CancellationToken cancellationToken)
    {
        var stations = await this.db.Stations.AsNoTracking()
            .Where(x => x.Location != null)
            .Select(x => new
            {
                Id = x.StationId,
                Location = x.Location!,
            })
            .ToListAsync(cancellationToken);

        var routes = await this.db.Routes.AsNoTracking()
            .Select(x => new
            {
                ALocation = x.Extremes.Single(y => !y.IsEnd).Station!.Location,
                BLocation = x.Extremes.Single(y => y.IsEnd).Station!.Location,
            })
            .ToListAsync(cancellationToken);

        var stationsGeoJson = new List<string>();
        var routesGeoJson = new List<string>();

        foreach (var station in stations)
        {
            stationsGeoJson.Add($"{{ \"type\": \"Point\", \"coordinates\": [{station.Location.Latitude},{station.Location.Latitude}] }}");
        }

        foreach (var route in routes)
        {
            if (route.ALocation != null && route.BLocation != null)
            {
                var points = new[] { route.ALocation!, route.BLocation! };

                var line = string.Join(',', points.Select(x => $"[{x.ToString()}]"));

                routesGeoJson.Add($"{{ \"type\": \"LineString\", \"coordinates\": [{line}] }}");
            }
        }

        return $"[{string.Join(',', routesGeoJson.Concat(stationsGeoJson))}]";
    }
}