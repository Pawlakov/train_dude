// <copyright file="CreateStationEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using System;

using TrainDude.Features.Generic;
using TrainDude.Features.Stations.Domain;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateStationEndpoint
{
    public const string Route = "/station/create";

    [WolverinePost(Route)]
    public static (CreatedResponse, IStartStream) Post(CreateStationCommand stationCommand)
    {
        var id = Guid.NewGuid();
        var domainEvent = StationAggregate.Make(id, stationCommand.NameGerman, stationCommand.NameGermanNew, stationCommand.NamePolish, stationCommand.NameRussian);

        var startStream = MartenOps.StartStream<StationAggregate>(id, domainEvent);

        var response = new CreatedResponse(id);

        return (response, startStream);
    }
}