// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Radii;

using System;

using FluentValidation;

using TrainDude.Commands.Requests.Radii;

public class CreateValidator
    : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.Speed)
            .GreaterThan(0)
            .WithMessage("A valid speed is required.");

        this.RuleFor(x => x.Minimum)
            .GreaterThan(0)
            .WithMessage("A valid minimum is required.");
    }
}