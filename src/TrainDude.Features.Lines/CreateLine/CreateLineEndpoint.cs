// <copyright file="CreateLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using System;

using TrainDude.Features.Lines.Contracts.CreateLine;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateLineEndpoint
{
    [WolverinePost(CreateLineCommand.TypeRoute)]
    public static (CreatedResult, IStartStream) Post(CreateLineCommand lineCommand)
    {
        var id = Guid.NewGuid();
        var domainEvent = LineAggregate.Make(id, lineCommand.Number, lineCommand.Letter);

        IStartStream startStream = MartenOps.StartStream<LineAggregate>(id, domainEvent);

        var response = new CreatedResult(id);

        return (response, startStream);
    }
}