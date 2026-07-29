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

using Microsoft.EntityFrameworkCore;

using TrainDude.Data;
using TrainDude.Data.Models;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;

internal class GetSegmentAntiradiusSeriesQueryHandler : IRequestHandler<GetSegmentAntiradiusSeriesQuery, string>
{
    private readonly NetworkDbContext db;

    public GetSegmentAntiradiusSeriesQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<string> Handle(GetSegmentAntiradiusSeriesQuery request, CancellationToken cancellationToken)
    {
        var aLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && !x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        var bLocation = await this.db.SegmentExtremes.Where(x => x.SegmentId == request.SegmentId && x.IsEnd).Select(x => x.Station!.Location).SingleOrDefaultAsync(cancellationToken);
        if (aLocation != null && bLocation != null)
        {
            var points = new[] { aLocation, bLocation };
            var segments = points.Segments().ToArray();

            var totalHaversine = segments.Haversine();
            var sampleLength = totalHaversine / request.Resolution;

            var currentSegment = segments[0];
            var currentPoint = segments[0].A;
            var samplePoints = new List<Location> { segments[0].A };
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