// <copyright file="GetRadiiQueryHandler.cs" company="Pawlakov">
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

using TrainDude.Data;
using TrainDude.Data.Models;
using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;

internal class GetRadiiQueryHandler : IRequestHandler<GetRadiiQuery, IEnumerable<RadiusSummaryDTO>>
{
    private readonly NetworkDbContext db;

    public GetRadiiQueryHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<RadiusSummaryDTO>> Handle(GetRadiiQuery request, CancellationToken cancellationToken)
    {
        var models = await this.db.Radii.AsNoTracking().ToListAsync(cancellationToken);
        var dtos = models
            .Select(x => new RadiusSummaryDTO
            {
                RadiusId = x.RadiusId,
                Speed = x.Speed,
                Minimum = x.Minimum,
                MaximumAntiradius = 1000 / (double)x.Minimum,
            })
            .ToList();

        return dtos;
    }
}