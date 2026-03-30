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

using MongoDB.Driver.GeoJsonObjectModel;

using TrainDude.Network.DTOs;
using TrainDude.Network.Extensions;
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, IEnumerable<RouteSummaryDTO>>
{
    private readonly RouteService routeService;
    private readonly StationService stationService;

    public GetRoutesQueryHandler(RouteService routeService, StationService stationService)
    {
        this.routeService = routeService;
        this.stationService = stationService;
    }

    public async Task<IEnumerable<RouteSummaryDTO>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
    {
        var nameMap = (await this.stationService.GetAll()).ToDictionary(x => x.Id, x => x);

        var models = await this.routeService.GetAll();
        var dtos = models
            .Select(x => new RouteSummaryDTO
            {
                Id = x.Id,
                NameA = nameMap[x.A.StationId].NameGerman,
                NameB = nameMap[x.B.StationId].NameGerman,
                Length = x.NominalLength,
                Haversine = x.MidPoints.Select(x => x.Location.Coordinates).Prepend(nameMap[x.A.StationId].Location?.Coordinates).Append(nameMap[x.B.StationId].Location?.Coordinates).Segments().Haversine(),
            })
            .ToList();

        return dtos;
    }
}