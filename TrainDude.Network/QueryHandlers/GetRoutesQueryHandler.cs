// <copyright file="GetRoutesQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;

internal class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, IEnumerable<RouteSummaryDTO>>
{
    private readonly NetworkDbContext db;

    public GetRoutesQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<RouteSummaryDTO>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
    {
        var nameMap = (await this.db.Stations.AsNoTracking().ToListAsync(cancellationToken)).ToDictionary(x => x.Id, x => x);

        var models = await this.db.Routes.AsNoTracking().ToListAsync(cancellationToken);
        var dtos = models
            .Select(x => this.HandleItem(x, nameMap))
            .ToList();

        return dtos;
    }

    private RouteSummaryDTO HandleItem(Route x, Dictionary<int, Station> nameMap)
    {
        var a = nameMap[x.Ends.Single(y => !y.IsEnd).StationId];
        var b = nameMap[x.Ends.Single(y => y.IsEnd).StationId];

        return new RouteSummaryDTO
        {
            Id = x.Id,
            NameA = a.NameGerman,
            NameB = b.NameGerman,
            Length = x.NominalLength,
            Haversine = new[] { a.Location, b.Location }.Segments().Haversine(),
        };
    }
}