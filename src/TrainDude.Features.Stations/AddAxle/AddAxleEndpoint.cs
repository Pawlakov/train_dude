// <copyright file="AddAxleEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using TrainDude.Features.Generic;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class AddAxleEndpoint
{
    public const string Route = "/station/axle/add";

    [AggregateHandler]
    [WolverinePost(Route)]
    public static (UpdatedResponse, Events) Handle(AddAxleCommand command, StationAggregate aggregate)
    {
        var domainEvent = aggregate.AddAxle();

        var response = new UpdatedResponse(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}