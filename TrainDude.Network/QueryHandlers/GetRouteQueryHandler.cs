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
            .Where(x => x.Id == request.Id)
            .Select(x => new
            {
                A = new
                {
                    x.Ends.Single(y => !y.IsEnd).StationId,
                    x.Ends.Single(y => !y.IsEnd).Station!.NameGerman,
                    x.Ends.Single(y => !y.IsEnd).Station!.Location,
                },
                B = new
                {
                    x.Ends.Single(y => y.IsEnd).StationId,
                    x.Ends.Single(y => y.IsEnd).Station!.NameGerman,
                    x.Ends.Single(y => y.IsEnd).Station!.Location,
                },
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (queryResult != null)
        {
            var dto = new RouteDetailsDTO
            {
                Id = request.Id,
                A = new StationSummaryDTO
                {
                    Id = queryResult.A.StationId,
                    Name = queryResult.A.NameGerman,
                    Location = queryResult.A.Location,
                },
                B = new StationSummaryDTO
                {
                    Id = queryResult.B.StationId,
                    Name = queryResult.B.NameGerman,
                    Location = queryResult.B.Location,
                },
            };

            return dto;
        }

        return null;
    }
}