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

    public DropCommandHandler(StationService stationService, RouteService routeService)
    {
        this.stationService = stationService;
        this.routeService = routeService;
    }

    public async Task Handle(DropCommand request, CancellationToken cancellationToken)
    {
        await this.routeService.DeleteAll();
        await this.stationService.DeleteAll();
    }
}