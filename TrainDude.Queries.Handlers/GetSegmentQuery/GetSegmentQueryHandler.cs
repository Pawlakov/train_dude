// <copyright file="GetSegmentQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.GetSegmentQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Queries.Data;
using TrainDude.Queries.Requests.GetSegmentQuery;

public sealed class GetSegmentQueryHandler
    : IQueryHandler<GetSegmentQuery, GetSegmentQueryResult?>
{
    private readonly ISegmentRepository db;

    public GetSegmentQueryHandler(ISegmentRepository db)
    {
        this.db = db;
    }

    public async ValueTask<GetSegmentQueryResult?> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await this.db.SegmentAggregates
            .Where(x => x.SegmentId == request.SegmentId)
            .Select(x => new
            {
                AName = x.A.NameGermanNew ?? x.A.NameGerman,
                ALocation = x.A.Location,
                BName = x.B.NameGermanNew ?? x.B.NameGerman,
                BLocation = x.B.Location,
                /*Vertices = x.Vertices.OrderBy(y => y.OrdinalId).ToList(),*/
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (queryResult == null)
        {
            return null;
        }

        var dto = new GetSegmentQueryResult
        {
            SegmentId = request.SegmentId,
            AName = queryResult.AName,
            BName = queryResult.BName,
            ALocation = queryResult.ALocation,
            BLocation = queryResult.BLocation,
            /*Vertices = queryResult.Vertices,*/
        };

        return dto;
    }
}