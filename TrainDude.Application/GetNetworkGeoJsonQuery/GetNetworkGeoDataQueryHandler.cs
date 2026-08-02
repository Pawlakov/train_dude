// <copyright file="GetNetworkGeoDataQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetNetworkGeoJsonQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetNetworkGeoJsonQuery;
using TrainDude.Data;

public sealed class GetNetworkGeoDataQueryHandler
    : IQueryHandler<GetNetworkGeoDataQuery, GetNetworkGeoDataQueryResult>
{
    private readonly INetworkDbContext db;

    public GetNetworkGeoDataQueryHandler(INetworkDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetNetworkGeoDataQueryResult> Handle(GetNetworkGeoDataQuery request, CancellationToken cancellationToken)
    {
        var stations = await this.db.Stations.AsNoTracking()
            .Where(x => x.Location != null)
            .Select(x => x.Location!.Value)
            .ToListAsync(cancellationToken);

        var segments = await this.db.Segments.AsNoTracking()
            .Where(x => x.Extremes.Count == 2 && x.Extremes.All(y => y.Station.Location != null))
            .Select(x => new
            {
                ALocation = x.Extremes.Single(y => !y.IsEnd).Station.Location!.Value,
                BLocation = x.Extremes.Single(y => y.IsEnd).Station.Location!.Value,
                Vertices = x.Vertices.OrderBy(y => y.OrdinalId).Select(y => y.Location).ToList(), // TODO dlaczego to się psuje
            })
            .ToListAsync(cancellationToken);

        return new GetNetworkGeoDataQueryResult
        {
            Stations = stations,
            Segments = segments
                .Select(x => new GetNetworkGeoDataQueryResultSegmentItem
                {
                    ALocation = x.ALocation,
                    BLocation = x.BLocation,
                    Vertices = x.Vertices,
                })
                .ToList(),
        };
    }
}