// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using FluentValidation;

using TrainDude.Commands.Requests.Stations;

public sealed class CreateValidator
    : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.NameGerman)
            .NotEmpty()
            .WithMessage("A valid name is required.")
            .MaximumLength(200);
    }
}