// <copyright file="StationCreatedNotificationHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections.Handlers;

using System;
using System.Threading;
using System.Threading.Tasks;

using Mediator;

using TrainDude.Shared.Notifications;
using TrainDude.Shared.Notifications.Stations;

public sealed class StationCreatedNotificationHandler
    : INotificationHandler<StationCreatedNotification>
{
    private readonly DataModelProjector projector;

    public StationCreatedNotificationHandler(DataModelProjector projector)
    {
        this.projector = projector;
    }

    public ValueTask Handle(StationCreatedNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        return new ValueTask(this.projector.RebuildAsync(cancellationToken));
    }
}