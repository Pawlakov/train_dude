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
using TrainDude.Shared.Values;

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
        var aEnd = new SegmentEnd(a.Id, command.AAxle, command.APole);
        var bEnd = new SegmentEnd(b.Id, command.BAxle, command.BPole);
        if (command.AAxle >= a.AxleCount)
        {
            throw new SegmentNoStationAxleException(command.Id, command.AAxle, a.Id, a.AxleCount);
        }

        if (command.BAxle >= b.AxleCount)
        {
            throw new SegmentNoStationAxleException(command.Id, command.BAxle, b.Id, b.AxleCount);
        }

        var domainEvent = SegmentAggregate.Make(command.Id, command.NominalLength, command.Tracks, aEnd, bEnd);

        IStartStream startStream = MartenOps.StartStream<SegmentAggregate>(domainEvent.Id, domainEvent);
        var nameMode = await SettingsAccessor.GetNameMode(session, cancellationToken);
        var nameSelector = StationNameResolver.GetNameSelector(nameMode);
        var aName = nameSelector(a);
        var bName = nameSelector(b);

        double? haversine = (a.Location, b.Location) switch
        {
            ({ } aLocation, { } bLocation) => aLocation.Haversine(bLocation),
            _ => null,
        };

        var response = new CreatedResponse(domainEvent.Id);
        var integrationEvent = new SegmentCreatedIntegrationEvent(domainEvent.Id, 1L, domainEvent.NominalLength, haversine, domainEvent.Tracks, new(a.Id, aName, a.Location), new(b.Id, bName, b.Location));

        return (response, startStream, new OutgoingMessages { integrationEvent });
    }
}