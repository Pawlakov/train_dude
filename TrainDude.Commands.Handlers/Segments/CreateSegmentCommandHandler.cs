// <copyright file="CreateSegmentCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Segments;

using TrainDude.Commands.Requests.Segments;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Segments;

using Wolverine;
using Wolverine.Marten;

public static class CreateSegmentCommandHandler
{
    public static (IStartStream, OutgoingMessages) Handle(CreateSegmentCommand command)
    {
        var domainEvent = Segment.Make(command.Id, command.NominalLength);

        var startStream = MartenOps.StartStream<Segment>(domainEvent.Id, domainEvent);

        var integrationEvent = new SegmentCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.NominalLength);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}