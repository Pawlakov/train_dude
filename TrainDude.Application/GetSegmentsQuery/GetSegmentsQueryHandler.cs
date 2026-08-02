// <copyright file="GetSegmentsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Extensions;
using TrainDude.Application.Requests.GetSegmentsQuery;
using TrainDude.Data;
using TrainDude.Data.Entities;

public sealed class GetSegmentsQueryHandler
    : IQueryHandler<GetSegmentsQuery, GetSegmentsQueryResult>
{
    private readonly INetworkDbContext db;

    public GetSegmentsQueryHandler(INetworkDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetSegmentsQueryResult> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
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
                Lines = x.Lines.Select(y => y.LineNumber.ToString() + y.LineLetter).ToList(),
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                NameA = x.A.Name,
                NameB = x.B.Name,
                Haversine = (x.A.Location != null && x.B.Location != null) ? x.Vertices.Select(y => y.Location).Prepend(x.A.Location.Value).Append(x.B.Location.Value).ToList().Segments().Haversine() : null,
                Lines = x.Lines,
            })
            .ToList();

        return new GetSegmentsQueryResult { Items = dtos };
    }
}