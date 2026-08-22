// <copyright file="CreateEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Segments;
using TrainDude.Domain;
using TrainDude.Domain.Segments;
using TrainDude.Domain.Stations;
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
        [ReadModel(nameof(CreateCommand.AId))] StationAggregate a,
        [ReadModel(nameof(CreateCommand.BId))] StationAggregate b,
        IDocumentSession session,
        CancellationToken cancellationToken = default)
    {
        var domainEvent = SegmentAggregate.Make(command.Id, command.NominalLength, a.Id, b.Id);

        IStartStream startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);
        var nameMode = await SettingsAccessor.GetNameMode(session, cancellationToken);
        var nameSelector = StationNameResolver.GetNameSelector(nameMode);
        var aName = nameSelector(a);
        var bName = nameSelector(b);

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new SegmentCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.NominalLength, new(a.Id, aName, a.Location), new(b.Id, bName, b.Location));

        return (response, startStream, new OutgoingMessages { integrationEvent });
    }
}