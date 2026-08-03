// <copyright file="GetNetworkQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetNetworkQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data;
using TrainDude.Queries.Requests.GetNetworkGeoJsonQuery;

public sealed class GetNetworkQueryHandler
    : IQueryHandler<GetNetworkQuery, GetNetworkQueryResult>
{
    private readonly IReadDbContext db;

    public GetNetworkQueryHandler(IReadDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetNetworkQueryResult> Handle(GetNetworkQuery request, CancellationToken cancellationToken)
    {
        var stations = await this.db.Stations.AsNoTracking()
            .Where(x => x.Location != null)
            .Select(x => x.Location!.Value)
            .ToListAsync(cancellationToken);

        var segments = await this.db.Segments.AsNoTracking()
            .Where(x => x.A.Location.HasValue && x.B.Location.HasValue)
            .Select(x => new
            {
                ALocation = x.A.Location!.Value,
                BLocation = x.B.Location!.Value,
                /*Vertices = x.Vertices.OrderBy(y => y.OrdinalId).Select(y => y.Location).ToList(),*/ // TODO dlaczego to się psuje
            })
            .ToListAsync(cancellationToken);

        return new GetNetworkQueryResult
        {
            Stations = stations,
            Segments = segments
                .Select(x => new GetNetworkQueryResultSegmentItem
                {
                    ALocation = x.ALocation,
                    BLocation = x.BLocation,
                    /*Vertices = x.Vertices,*/
                })
                .ToList(),
        };
    }
}