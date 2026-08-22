// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Stations;
using TrainDude.Commands.Endpoints.Base;

public sealed class CreateValidator
    : BaseDomainValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.NameGerman)
            .NotEmpty()
            .WithMessage("A valid name is required.")
            .MaximumLength(200);
    }
}