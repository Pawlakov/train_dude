// <copyright file="GetRouteQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data;
using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;

internal class GetSegmentQueryHandler : IRequestHandler<GetSegmentQuery, SegmentDetailsDTO>
{
    private readonly NetworkDbContext db;

    public GetSegmentQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<SegmentDetailsDTO?> Handle(GetSegmentQuery request, CancellationToken cancellationToken)
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

        var dto = new SegmentDetailsDTO
        {
            SegmentId = request.SegmentId,
            AName = queryResult.A.NameGerman,
            BName = queryResult.B.NameGerman,
            ALocation = queryResult.A.Location!,
        };

        return dto;
    }
}