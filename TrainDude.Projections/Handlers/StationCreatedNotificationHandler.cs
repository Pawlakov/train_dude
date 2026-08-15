// <copyright file="StationCreatedNotificationHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using TrainDude.Domain.Events.Stations;
using TrainDude.Shared.Notifications;

public sealed class StationCreatedNotificationHandler
    : INotificationHandler<StationCreated>
{
    private readonly DataModelProjector projector;

    public StationCreatedNotificationHandler(DataModelProjector projector)
    {
        this.projector = projector;
    }

    public ValueTask Handle(StationCreated notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        return new ValueTask(this.projector.RebuildAsync(cancellationToken));
    }
}