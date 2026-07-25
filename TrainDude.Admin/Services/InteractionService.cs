// <copyright file="InteractionService.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Admin.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Hosting;

using TrainDude.Admin.Commands;

public class InteractionService
    : BackgroundService
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly IMediator mediator;

    public InteractionService(IHostApplicationLifetime lifetime, IMediator mediator)
    {
        this.lifetime = lifetime;
        this.mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            Console.WriteLine("What do we do, boss?");
            Console.WriteLine("1. Drop the base!");
            Console.WriteLine("2. Seed the base!");

            var decision = Console.ReadKey();

            if (decision.KeyChar == '1')
            {
                await this.mediator.Send(new DropCommand(), stoppingToken);
                this.lifetime.StopApplication();
                break;
            }

            if (decision.KeyChar == '2')
            {
                await this.mediator.Send(new SeedCommand(), stoppingToken);
                this.lifetime.StopApplication();
                break;
            }
        }
    }
}