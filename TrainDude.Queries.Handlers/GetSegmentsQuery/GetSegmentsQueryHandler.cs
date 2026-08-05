// <copyright file="GetSegmentsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetSegmentsQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data;
using TrainDude.Queries.Requests.GetSegmentsQuery;

public sealed class GetSegmentsQueryHandler
    : IQueryHandler<GetSegmentsQuery, GetSegmentsQueryResult>
{
    private readonly ISegmentRepository db;

    public GetSegmentsQueryHandler(ISegmentRepository db)
    {
        this.db = db;
    }

    public async ValueTask<GetSegmentsQueryResult> Handle(GetSegmentsQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.SegmentAggregates.AsNoTracking()
            .Select(x => new
            {
                x.SegmentId,
                x.NominalLength,
                AName = x.A.NameGermanNew ?? x.A.NameGerman,
                ALocation = x.A.Location,
                BName = x.B.NameGermanNew ?? x.B.NameGerman,
                BLocation = x.B.Location,
                /*Vertices = x.Vertices.OrderBy(y => y.OrdinalId).ToList(),*/
            })
            .ToListAsync(cancellationToken);

        var dtos = models
            .Select(x => new GetSegmentsQueryResultItem
            {
                SegmentId = x.SegmentId,
                Length = x.NominalLength,
                NameA = x.AName,
                NameB = x.BName,
                /*Haversine = (x.A.Location != null && x.B.Location != null) ? x.Vertices.Select(y => y.Location).Prepend(x.A.Location.Value).Append(x.B.Location.Value).ToList().Segments().Haversine() : null,*/
            })
            .ToList();

        return new GetSegmentsQueryResult { Items = dtos };
    }
}