// <copyright file="CreateSegmentCommandHandler.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Segments;

using System;
using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Handlers.Services;
using TrainDude.Commands.Requests.Segments;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Segments;

using Wolverine;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class CreateSegmentCommandHandler
{
    public static void Validate(CreateSegmentCommand command)
    {
        if (command.AId == Guid.Empty || command.BId == Guid.Empty)
        {
            throw new InvalidOperationException("A valid extrema IDs is required.");
        }
    }

    public static async Task<(IStartStream, OutgoingMessages)> HandleAsync(
        CreateSegmentCommand command,
        [ReadModel(nameof(CreateSegmentCommand.AId))] Station a,
        [ReadModel(nameof(CreateSegmentCommand.BId))] Station b,
        SettingsService settingsService,
        CancellationToken cancellationToken = default)
    {
        var domainEvent = Segment.Make(command.Id, command.NominalLength, a.Id, b.Id);

        var startStream = MartenOps.StartStream<Segment>(domainEvent.Id, domainEvent);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var aName = nameSelector(a);
        var bName = nameSelector(b);

        var integrationEvent = new SegmentCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.NominalLength, new(a.Id, aName, a.Location), new(b.Id, bName, b.Location));

        return (startStream, new OutgoingMessages { integrationEvent });
    }
}