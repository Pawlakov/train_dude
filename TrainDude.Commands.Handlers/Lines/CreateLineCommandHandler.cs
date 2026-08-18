// <copyright file="CreateLineCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Lines;

using System;

using TrainDude.Commands.Requests.Lines;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Lines;

using Wolverine;
using Wolverine.Marten;

public static class CreateLineCommandHandler
{
    public static void Validate(CreateLineCommand command)
    {
        if (command.Number == 0)
        {
            throw new InvalidOperationException("A valid number is required.");
        }

        if (command.Letter is > 'z' or < 'a')
        {
            throw new InvalidOperationException("A valid letter is required.");
        }
    }

    public static (IStartStream, OutgoingMessages) Handle(CreateLineCommand command)
    {
        var domainEvent = Line.Make(command.Id, command.Number, command.Letter);

        var startStream = MartenOps.StartStream<Line>(command.Id, domainEvent);

        var integrationEvent = new LineCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.LineNumber, domainEvent.LineLetter);

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}