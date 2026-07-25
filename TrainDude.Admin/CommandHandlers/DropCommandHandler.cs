// <copyright file="DropCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Admin.CommandHandlers;

using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore;

using TrainDude.Admin.Commands;
using TrainDude.Data.Models;

internal class DropCommandHandler : IRequestHandler<DropCommand>
{
    private readonly NetworkDbContext db;

    public DropCommandHandler(NetworkDbContext db)
    {
        this.db = db;
    }

    public async Task Handle(DropCommand request, CancellationToken cancellationToken)
    {
        await this.db.Radii.ExecuteDeleteAsync(cancellationToken);
        await this.db.Routes.ExecuteDeleteAsync(cancellationToken);
        await this.db.Stations.ExecuteDeleteAsync(cancellationToken);
    }
}