// <copyright file="SetLocationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System.Threading.Tasks;

using TrainDude.Features.Generic;
using TrainDude.Features.Stations.Domain;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class SetLocationEndpoint
{
    public const string Route = "/station/location/set";

    [AggregateHandler]
    [WolverinePost(Route)]
    public static (UpdatedResponse, Events) Post(SetLocationCommand command, StationAggregate aggregate)
    {
        var domainEvent = aggregate.SetLocation(command.Location);

        var response = new UpdatedResponse(aggregate.Version + 1);

        return (response, new Events { domainEvent });
    }
}