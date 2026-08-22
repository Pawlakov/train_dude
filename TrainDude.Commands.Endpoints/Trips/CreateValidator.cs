// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Trips;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Trips;
using TrainDude.Commands.Endpoints.Base;

public sealed class CreateValidator
    : BaseDomainValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("A valid number is required.");
    }
}