// <copyright file="AppendStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Domain;
using TrainDude.Domain.Lines;
using TrainDude.Domain.Stations;
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
        [WriteModel(Required = true)] LineAggregate aggregate,
        [ReadModel(nameof(AppendStationCommand.StationId))] StationAggregate stationAggregate,
        IDocumentSession session,
        CancellationToken cancellationToken = default)
    {
        var domainEvent = aggregate.AppendStation(stationAggregate.Id);
        var nameMode = await SettingsAccessor.GetNameMode(session, cancellationToken);
        var nameSelector = StationNameResolver.GetNameSelector(nameMode);
        var stationName = nameSelector(stationAggregate);

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new LineStationAppendedIntegrationEvent(domainEvent.Id, aggregate.Version + 1, new(stationAggregate.Id, stationName, stationAggregate.Location));

        return (response, new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}