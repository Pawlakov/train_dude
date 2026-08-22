// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Radii;

using System.Threading.Tasks;

using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Radii;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Radii;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command)
    {
        var domainEvent = Radius.Make(command.Id, command.Speed, command.Minimum);

        IStartStream startStream = MartenOps.StartStream<Radius>(domainEvent.Id, domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new RadiusCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.Speed, domainEvent.Minimum);

        return Task.FromResult((response, startStream, new OutgoingMessages { integrationEvent }));
    }
}