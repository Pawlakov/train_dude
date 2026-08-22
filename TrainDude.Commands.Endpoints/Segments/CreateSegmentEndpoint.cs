// <copyright file="CreateSegmentEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using System.Threading;
using System.Threading.Tasks;

using TrainDude.Commands.Endpoints.Services;
using TrainDude.Commands.Requests.Generic;
using TrainDude.Commands.Requests.Segments;
using TrainDude.Domain.Documents;
using TrainDude.Integration.Events.Segments;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;
using Wolverine.Persistence.EventSourcing;

public static class CreateEndpoint
{
    [WolverinePost(CreateCommand.Route)]
    public static async Task<(CreatedResponse, IStartStream, OutgoingMessages)> Post(
        CreateCommand command,
        [ReadModel(nameof(CreateCommand.AId))] Station a,
        [ReadModel(nameof(CreateCommand.BId))] Station b,
        SettingsService settingsService,
        CancellationToken cancellationToken = default)
    {
        var domainEvent = Segment.Make(command.Id, command.NominalLength, a.Id, b.Id);

        IStartStream startStream = MartenOps.StartStream<Segment>(domainEvent.Id, domainEvent);

        var nameSelector = await settingsService.GetNameSelector(cancellationToken);
        var aName = nameSelector(a);
        var bName = nameSelector(b);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new SegmentCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.NominalLength, new(a.Id, aName, a.Location), new(b.Id, bName, b.Location));

        return (response, startStream, new OutgoingMessages { integrationEvent });
    }
}