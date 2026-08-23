// <copyright file="SetCourseEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using System.Linq;
using System.Threading.Tasks;

using Marten;

using TrainDude.Commands.Contracts.Generic;
using TrainDude.Commands.Contracts.Segments;
using TrainDude.Domain.Segments;
using TrainDude.Domain.Stations;
using TrainDude.Integration.Events.Segments;
using TrainDude.Shared.Values;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class SetCourseEndpoint
{
    [AggregateHandler]
    [WolverinePost(SetCourseCommand.Route)]
    public static async Task<(UpdatedResponse, Events, OutgoingMessages)> Post(SetCourseCommand command, SegmentAggregate aggregate, IDocumentSession session)
    {
        var domainEvent = aggregate.SetCourse(command.Course);

        var a = await session.Events.FetchLatest<StationAggregate>(aggregate.AId);
        var b = await session.Events.FetchLatest<StationAggregate>(aggregate.BId);
        double? haversine = (a?.Location, b?.Location) switch
        {
            ({} aLocation, {} bLocation) => command.Course.Prepend(aLocation).Append(bLocation).Haversine(),
            _ => null,
        };

        var response = new UpdatedResponse(aggregate.Version + 1);
        var integrationEvent = new SegmentCourseSetIntegrationEvent(domainEvent.Id, aggregate.Version + 1, domainEvent.Course, haversine);

        return (response, new Events { domainEvent }, new OutgoingMessages { integrationEvent });
    }
}