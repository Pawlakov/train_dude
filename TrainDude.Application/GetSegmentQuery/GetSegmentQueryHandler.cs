// <copyright file="GetSegmentQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.GetSegmentQuery;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Application.Requests.GetSegmentQuery;
using TrainDude.Application.Requests.Values;
using TrainDude.Data;

internal class GetSegmentQueryHandler
    : IRequestHandler<GetSegmentQuery, GetSegmentQueryResult?>
{
    private readonly NetworkDbContext db;

    public GetSegmentQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<GetSegmentQueryResult?> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await this.db.Segments
            .Where(x => x.SegmentId == request.SegmentId)
            .Select(x => new
            {
                A = new
                {
                    x.Extremes.Single(y => !y.IsEnd).Station!.NameGerman,
                    x.Extremes.Single(y => !y.IsEnd).Station!.Location,
                },
                B = new
                {
                    x.Extremes.Single(y => y.IsEnd).Station!.NameGerman,
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
            AName = queryResult.A.NameGerman,
            BName = queryResult.B.NameGerman,
            ALocation = new GeodeticPosition(queryResult.A.Location!.Longitude, queryResult.A.Location!.Latitude),
        };

        return dto;
    }
}