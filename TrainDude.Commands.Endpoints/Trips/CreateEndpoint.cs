// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Trips;

using System.Threading.Tasks;

using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Trips;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Trips;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command, IMessageBus bus)
    {
        var domainEvent = Trip.Make(command.Id, command.Number);

        IStartStream startStream = MartenOps.StartStream<Trip>(command.Id, domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new TripCreatedIntegrationEvent(command.Id, 1L, command.Number);

        return Task.FromResult((response, startStream, new OutgoingMessages { integrationEvent }));
    }
}