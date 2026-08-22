// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Segments;

using System;

using FluentValidation;

using TrainDude.Commands.Requests.Segments;

public class CreateValidator
    : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.AId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid A id is required.");

        this.RuleFor(x => x.BId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid B id is required.");
    }
}