// <copyright file="GetSegmentGeoJsonQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentGeoJsonQuery;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetSegmentGeoJsonQuery;
using TrainDude.Data;

public sealed class GetSegmentGeoDataQueryHandler
    : IQueryHandler<GetSegmentGeoDataQuery, GetSegmentGeoDataQueryResult>
{
    private readonly INetworkDbContext db;

    public GetSegmentGeoDataQueryHandler(INetworkDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetSegmentGeoDataQueryResult> Handle(GetSegmentGeoDataQuery request, CancellationToken cancellationToken)
    {
        var stations = await this.db.Segments.AsNoTracking()
            .Where(x => x.SegmentId == request.SegmentId)
            .SelectMany(x => x.Extremes)
            .Where(x => x.Station.Location != null)
            .Select(x => x.Station.Location!.Value)
            .ToListAsync(cancellationToken);

        var segment = await this.db.Segments.AsNoTracking()
            .Where(x => x.SegmentId == request.SegmentId && x.Extremes.Count == 2 && x.Extremes.All(y => y.Station.Location != null))
            .Select(x => new
            {
                ALocation = x.Extremes.Single(y => !y.IsEnd).Station.Location!.Value,
                BLocation = x.Extremes.Single(y => y.IsEnd).Station.Location!.Value,
                Vertices = x.Vertices.OrderBy(y => y.OrdinalId).Select(y => y.Location).ToList(),
            })
            .SingleOrDefaultAsync(cancellationToken);

        return new GetSegmentGeoDataQueryResult
        {
            Stations = stations,
            Segment = new GetSegmentGeoDataQueryResultSegmentItem
            {
                ALocation = segment.ALocation,
                BLocation = segment.BLocation,
                Vertices = segment.Vertices,
            },
        };
    }
}