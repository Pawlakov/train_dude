// <copyright file="CreateLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.CreateLine;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateLineEndpoint
{
    [WolverinePost(CreateLineCommand.TypeRoute)]
    [Tags("Lines")]
    public static (CreatedResult, IStartStream) Handle(CreateLineCommand lineCommand, ClaimsPrincipal user)
    {
        var id = Guid.NewGuid();
        var domainEvent = LineAggregate.Make(id, user.GetSubject(), lineCommand.Number, lineCommand.Letter);

        IStartStream startStream = MartenOps.StartStream<LineAggregate>(id, domainEvent);

        var response = new CreatedResult(id);

        return (response, startStream);
    }
}