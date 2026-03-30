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

using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetRadiiQueryHandler : IRequestHandler<GetRadiiQuery, IEnumerable<RadiusSummaryDTO>>
{
    private readonly RadiusService radiusService;

    public GetRadiiQueryHandler(RadiusService radiusService)
    {
        this.radiusService = radiusService;
    }

    public async Task<IEnumerable<RadiusSummaryDTO>> Handle(GetRadiiQuery request, CancellationToken cancellationToken)
    {
        var models = await this.radiusService.GetAll();
        var dtos = models
            .Select(x => new RadiusSummaryDTO
            {
                Speed = x.Speed,
                Minimum = x.Minimum,
                MaximumAntiradius = 1000 / (double)x.Minimum,
            })
            .ToList();

        return dtos;
    }
}