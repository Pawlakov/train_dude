// <copyright file="GetSegmentsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Extensions;
using TrainDude.Application.Requests.GetSegmentsQuery;
using TrainDude.Application.Requests.Values;
using TrainDude.Data;
using TrainDude.Data.Entities;

internal class GetSegmentsQueryHandler
    : IRequestHandler<GetSegmentsQuery, GetSegmentsQueryResult>
{
    private readonly NetworkDbContext db;

    public GetSegmentsQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<GetSegmentsQueryResult> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.Segments.AsNoTracking()
            .Select(x => new
            {
                x.SegmentId,
                x.NominalLength,
                A = new
                {
                    Name = x.Extremes.Where(y => !y.IsEnd).Select(y => y.Station.NameGermanNew ?? y.Station.NameGerman).Single(),
                    Location = x.Extremes.Where(y => !y.IsEnd).Select(y => y.Station.Location).Single(),
                },
                B = new
                {
                    Name = x.Extremes.Where(y => y.IsEnd).Select(y => y.Station.NameGermanNew ?? y.Station.NameGerman).Single(),
                    Location = x.Extremes.Where(y => y.IsEnd).Select(y => y.Station.Location).Single(),
                },
                Vertices = x.Vertices.OrderBy(y => y.OrdinalId).ToList(),
                Charts = x.Charts.Select(y => y.ChartId).ToList(),
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                NameA = x.A.Name,
                NameB = x.B.Name,
                Haversine = (x.A.Location != null && x.B.Location != null) ? x.Vertices.Cast<Location>().Prepend(x.A.Location).Append(x.B.Location).Select(y => new GeodeticPosition{ Longitude = y.Longitude, Latitude = y.Latitude }).ToList().Segments().Haversine() : null,
                Charts = x.Charts,
            })
            .ToList();

        return new GetSegmentsQueryResult { Items = dtos };
    }
}