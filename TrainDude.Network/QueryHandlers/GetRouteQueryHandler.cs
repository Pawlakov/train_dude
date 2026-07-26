// <copyright file="GetRouteQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;

internal class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, RouteDetailsDTO>
{
    private readonly NetworkDbContext db;

    public GetRouteQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<RouteDetailsDTO?> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await this.db.Routes
            .Where(x => x.SegmentId == request.Id)
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

        var dto = new RouteDetailsDTO
        {
            Id = request.Id,
            AName = queryResult.A.NameGerman,
            BName = queryResult.A.NameGerman,
            ALocation = queryResult.A.Location!,
        };

        return dto;
    }
}