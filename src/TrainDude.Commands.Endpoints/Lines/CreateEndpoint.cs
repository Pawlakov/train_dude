// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Contracts.Base;
using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Lines;
using TrainDude.Domain.Lines;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command)
    {
        var domainEvent = LineAggregate.Make(command.Id, command.Number, command.Letter);

        IStartStream startStream = MartenOps.StartStream<LineAggregate>(domainEvent.Id, domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new LineCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.LineNumber, domainEvent.LineLetter);

        return Task.FromResult((response, startStream, new OutgoingMessages { integrationEvent }));
    }
}