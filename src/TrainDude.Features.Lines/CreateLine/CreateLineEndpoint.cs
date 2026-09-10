// <copyright file="CreateLineEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using System;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Lines.Contracts.CreateLine;
using TrainDude.Features.Lines.Contracts.GetLine;
using TrainDude.Features.Lines.Domain;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateLineEndpoint
{
    [WolverinePost(CreateLineCommand.TypeRoute)]
    [Tags("Lines")]
    public static (IResult, IStartStream) Handle(CreateLineCommand command, ClaimsPrincipal user)
    {
        var domainEvent = LineAggregate.Make(command.LineId, user.GetSubject(), command.Number, command.Letter);

        var startStream = MartenOps.StartStream<LineAggregate>(domainEvent.Id, domainEvent);

        var getUrl = string.Format(GetLineQuery.Route, domainEvent.Id);
        var response = new CreationResponse(getUrl);
        var result = Results.Created(getUrl, response);

        return (result, startStream);
    }
}