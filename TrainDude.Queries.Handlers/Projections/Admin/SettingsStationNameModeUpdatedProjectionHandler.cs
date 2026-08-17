// <copyright file="SettingsStationNameModeUpdatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Projections.Admin;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Integration.Events.Admin;

public static class SettingsStationNameModeUpdatedProjectionHandler
{
    public static Task Handle(SettingsStationNameModeUpdatedIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        // TODO Actually do the thing.
        return Task.CompletedTask;
    }
}