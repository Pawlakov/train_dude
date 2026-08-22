// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Endpoints.Services;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Stations;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Stations;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static async Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command, SettingsService settingsService, CancellationToken cancellationToken = default)
    {
        var domainEvent = Station.Make(command.Id, command.NameGerman, command.NameGermanNew, command.NamePolish, command.NameRussian);

        IStartStream startStream = MartenOps.StartStream<Station>(domainEvent.Id, domainEvent);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var stationName = nameSelector(domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new StationCreatedIntegrationEvent(domainEvent.Id, 1L, stationName);

        return (response, startStream, new OutgoingMessages { integrationEvent });
    }
}