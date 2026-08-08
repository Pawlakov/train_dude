// <copyright file="DataChangedNotificationHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Projections;

using System.Threading;
using System.Threading.Tasks;

using Mediator;

using TrainDude.Shared.Notifications;

public sealed class DataChangedNotificationHandler
    : INotificationHandler<DataChangedNotification>
{
    private readonly DataModelProjector projector;

    public DataChangedNotificationHandler(DataModelProjector projector)
    {
        this.projector = projector;
    }

    public ValueTask Handle(DataChangedNotification notification, CancellationToken cancellationToken)
    {
        return new ValueTask(this.projector.RebuildAsync(cancellationToken));
    }
}