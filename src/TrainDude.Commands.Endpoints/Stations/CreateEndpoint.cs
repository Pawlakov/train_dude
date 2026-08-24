// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Stations;
using TrainDude.Domain;
using TrainDude.Domain.Stations;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static async Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        var domainEvent = StationAggregate.Make(command.Id, command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        IStartStream startStream = MartenOps.StartStream<StationAggregate>(domainEvent.Id, domainEvent);

        var nameMode = await SettingsAccessor.GetNameMode(session, cancellationToken);
        var nameSelector = StationNameResolver.GetNameSelector(nameMode);
        var stationName = nameSelector(domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new StationCreatedIntegrationEvent(domainEvent.Id, 1L, stationName);

        return (response, startStream, new OutgoingMessages { integrationEvent });
    }
}