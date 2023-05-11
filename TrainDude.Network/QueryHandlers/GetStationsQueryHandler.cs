// <copyright file="GetStationsQueryHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.QueryHandlers;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TrainDude.Network.DTOs;
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetStationsQueryHandler : IRequestHandler<GetStationsQuery, IEnumerable<StationSummaryDTO>>
{
    private readonly StationService networkService;

    public GetStationsQueryHandler(StationService networkService)
    {
        this.networkService = networkService;
    }

    public async Task<IEnumerable<StationSummaryDTO>> Handle(GetStationsQuery request, CancellationToken cancellationToken)
    {
        var models = await this.networkService.GetAll();
        var dtos = models
            .Select(x => new StationSummaryDTO { Id = x.Id, Name = x.NameGerman })
            .ToList();

        return dtos;
    }
}