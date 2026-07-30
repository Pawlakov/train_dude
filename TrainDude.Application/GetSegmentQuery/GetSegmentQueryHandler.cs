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
            ALocation = new GeodeticPosition(queryResult.A.Location!.Longitude, queryResult.A.Location!.Latitude),
            BLocation = new GeodeticPosition(queryResult.B.Location!.Longitude, queryResult.B.Location!.Latitude),
        };

        return dto;
    }
}