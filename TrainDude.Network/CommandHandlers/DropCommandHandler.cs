// <copyright file="DropCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.CommandHandlers;

using System.Threading;
using System.Threading.Tasks;

using MediatR;
using TrainDude.Network.Commands;
using TrainDude.Network.Services;

internal class DropCommandHandler : IRequestHandler<DropCommand>
{
    private readonly StationService stationService;
    private readonly RouteService routeService;
    private readonly RadiusService radiusService;

    public DropCommandHandler(StationService stationService, RouteService routeService, RadiusService radiusService)
    {
        this.stationService = stationService;
        this.routeService = routeService;
        this.radiusService = radiusService;
    }

    public async Task Handle(DropCommand request, CancellationToken cancellationToken)
    {
        await this.radiusService.DeleteAll();
        await this.routeService.DeleteAll();
        await this.stationService.DeleteAll();
    }
}