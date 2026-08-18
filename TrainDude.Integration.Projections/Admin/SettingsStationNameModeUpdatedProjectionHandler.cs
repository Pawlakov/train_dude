// <copyright file="SettingsStationNameModeUpdatedProjectionHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Integration.Projections.Admin;

using System;
using System.Threading;
using System.Threading.Tasks;

using LiteDB;

using TrainDude.Integration.Events.Admin;
using TrainDude.Queries.Data.Documents;

public static class SettingsStationNameModeUpdatedProjectionHandler
{
    public static Task Handle(SettingsStationNameModeUpdatedIntegrationEvent @event, ILiteCollection<Station> stationRepository, CancellationToken cancellationToken = default)
    {
        foreach (var station in stationRepository.FindAll())
        {
            station.Name = @event.NewNames[station.StationId];
            stationRepository.Update(station);
        }

        // TODO I guess it would be nice to save the mode somewhere so it can be loaded in settings
        return Task.CompletedTask;
    }
}