// <copyright file="AppendStationCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Lines;

using System;
using System.Threading;
using System.Threading.Tasks;

using JasperFx.Events;

using TrainDude.Commands.Handlers.Services;
using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;
using TrainDude.Integration.Values;

using Wolverine;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendStationCommandHandler
{
    public static void Validate(AppendStationCommand command)
    {
        if (command.StationId == Guid.Empty)
        {
            throw new InvalidOperationException("A valid station ID is required.");
        }
    }

    [AggregateHandler]
    public static async Task<(Events, OutgoingMessages)> HandleAsync(AppendStationCommand command, Line aggregate, [ReadModel(nameof(AppendStationCommand.StationId))] Station station, SettingsService settingsService, CancellationToken cancellationToken = default)
    {
        var domainEvent = aggregate.AppendStation(station.Id);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var stationName = nameSelector(station);

        var integrationEvent = new LineStationAppendedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(station.Id, stationName, station.Location));

        return (new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}