// <copyright file="CreateLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using System;
using System.Threading.Tasks;

using TrainDude.Features.Generic;
using TrainDude.Features.Lines.Domain;

using Wolverine;
using Wolverine.Http;
using Wolverine.Marten;

public static class CreateLineEndpoint
{
    public const string Route = "/line/create";

    [WolverinePost(Route)]
    public static (CreatedResponse, IStartStream) Post(CreateLineCommand lineCommand)
    {
        var id = Guid.NewGuid();
        var domainEvent = LineAggregate.Make(id, lineCommand.Number, lineCommand.Letter);

        IStartStream startStream = MartenOps.StartStream<LineAggregate>(id, domainEvent);

        var response = new CreatedResponse(id);

        return (response, startStream);
    }
}