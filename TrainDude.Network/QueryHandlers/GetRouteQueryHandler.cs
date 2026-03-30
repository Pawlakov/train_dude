// <copyright file="GetRouteQueryHandler.cs" company="Pawlakov">
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
using TrainDude.Network.Queries;
using TrainDude.Network.Services;

internal class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, RouteDetailsDTO>
{
    private readonly RouteService routeService;
    private readonly StationService stationService;

    public GetRouteQueryHandler(RouteService routeService, StationService stationService)
    {
        this.routeService = routeService;
        this.stationService = stationService;
    }

    public async Task<RouteDetailsDTO?> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var model = await this.routeService.Get(request.Id);
        if (model != null)
        {
            var a = await this.stationService.Get(model.A.StationId);
            var b = await this.stationService.Get(model.B.StationId);

            var dto = new RouteDetailsDTO
            {
                Id = model.Id,
                A = new StationSummaryDTO
                {
                    Id = a.Id,
                    Name = a.NameGerman,
                    Latitude = a.Location?.Coordinates?.Latitude,
                    Longitude = a.Location?.Coordinates?.Longitude,
                },
                B = new StationSummaryDTO
                {
                    Id = b.Id,
                    Name = b.NameGerman,
                    Latitude = b.Location?.Coordinates?.Latitude,
                    Longitude = b.Location?.Coordinates?.Longitude,
                },
            };

            return dto;
        }

        return null;
    }
}