// <copyright file="GetStationsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;

internal class GetStationsQueryHandler : IRequestHandler<GetStationsQuery, IEnumerable<StationSummaryDTO>>
{
    private readonly NetworkDbContext db;

    public GetStationsQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<StationSummaryDTO>> Handle(GetStationsQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.Stations.AsNoTracking().ToListAsync(cancellationToken);
        var dtos = models
            .Select(x => new StationSummaryDTO { Id = x.Id, Name = x.NameGerman, Location = x.Location })
            .ToList();

        return dtos;
    }
}