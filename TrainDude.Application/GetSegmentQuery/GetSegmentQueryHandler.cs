// <copyright file="GetSegmentQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetSegmentQuery;
using TrainDude.Data;

public sealed class GetSegmentQueryHandler
    : IQueryHandler<GetSegmentQuery, GetSegmentQueryResult?>
{
    private readonly INetworkDbContext db;

    public GetSegmentQueryHandler(INetworkDbContext db)
    {
        this.db = db;
    }

    public async ValueTask<GetSegmentQueryResult?> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await this.db.Segments
            .Where(x => x.SegmentId == request.SegmentId)
            .Select(x => new
            {
                A = new
                {
                    Name = x.Extremes.Single(y => !y.IsEnd).Station!.NameGermanNew ?? x.Extremes.Single(y => !y.IsEnd).Station!.NameGerman,
                    x.Extremes.Single(y => !y.IsEnd).Station!.Location,
                },
                B = new
                {
                    Name = x.Extremes.Single(y => y.IsEnd).Station!.NameGermanNew ?? x.Extremes.Single(y => y.IsEnd).Station!.NameGerman,
                    x.Extremes.Single(y => y.IsEnd).Station!.Location,
                },
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (queryResult == null)
        {
            return null;
        }

        var dto = new GetSegmentQueryResult
        {
            SegmentId = request.SegmentId,
            AName = queryResult.A.Name,
            BName = queryResult.B.Name,
            ALocation = queryResult.A.Location,
            BLocation = queryResult.B.Location,
        };

        return dto;
    }
}