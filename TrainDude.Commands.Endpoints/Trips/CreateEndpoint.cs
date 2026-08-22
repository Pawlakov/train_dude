// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Trips;

using System.Threading.Tasks;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Trips;
using TrainDude.Domain.Trips;
using TrainDude.Integration.Events.Trips;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(CreateCommand command)
    {
        var domainEvent = TripAggregate.Make(command.Id, command.Number);

        IStartStream startStream = MartenOps.StartStream<TripAggregate>(domainEvent.Id, domainEvent);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new TripCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.TripNumber);

        return Task.FromResult((response, startStream, new OutgoingMessages { integrationEvent }));
    }
}