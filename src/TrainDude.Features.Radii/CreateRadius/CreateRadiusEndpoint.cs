// <copyright file="CreateRadiusEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.CreateRadius;

using System;

using TrainDude.Features.Radii.Contracts.CreateRadius;
using TrainDude.Features.Radii.Domain;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;
using Wolverine.Marten;

public static class CreateRadiusEndpoint
{
    [WolverinePost(CreateRadiusCommand.TypeRoute)]
    public static (CreatedResult, IStartStream) Handle(CreateRadiusCommand command)
    {
        var id = Guid.NewGuid();
        var domainEvent = RadiusAggregate.Make(id, command.Speed, command.Minimum);

        var startStream = MartenOps.StartStream<RadiusAggregate>(id, domainEvent);

        var response = new CreatedResult(domainEvent.Id);

        return (response, startStream);
    }
}