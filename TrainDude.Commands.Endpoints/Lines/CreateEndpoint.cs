// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Base;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command, IMessageBus bus)
    {
        var domainEvent = Line.Make(command.Id, command.Number, command.Letter);

        IStartStream startStream = MartenOps.StartStream<Line>(command.Id, domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new LineCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.LineNumber, domainEvent.LineLetter);

        return Task.FromResult((response, startStream, new OutgoingMessages { integrationEvent }));
    }
}