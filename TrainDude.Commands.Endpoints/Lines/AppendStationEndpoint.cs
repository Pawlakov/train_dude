// <copyright file="AppendStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Endpoints.Services;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class AppendStationEndpoint
{
    [AggregateHandler]
    [WolverinePost(AppendStationCommand.Route)]
    public static async Task<(UpdatedResponse, Events, OutgoingMessages)> Post(
        AppendStationCommand command,
        [WriteModel(Required = true)] Line aggregate,
        [ReadModel(nameof(AppendStationCommand.StationId))] Station station,
        SettingsService settingsService,
        CancellationToken cancellationToken = default)
    {
        var domainEvent = aggregate.AppendStation(station.Id);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var stationName = nameSelector(station);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new LineStationAppendedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(station.Id, stationName, station.Location));

        return (response, new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}