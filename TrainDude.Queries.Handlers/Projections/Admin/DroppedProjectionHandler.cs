// <copyright file="DroppedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Projections.Admin;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Integration.Events.Admin;

public static class DroppedProjectionHandler
{
    public static Task Handle(DroppedIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        // TODO Obliterate the database.
        return Task.CompletedTask;
    }
}