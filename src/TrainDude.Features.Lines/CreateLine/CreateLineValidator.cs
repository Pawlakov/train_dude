// <copyright file="CreateLineValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using System;

using FluentValidation;

public sealed class CreateLineValidator
    : AbstractValidator<CreateLineCommand>
{
    public CreateLineValidator()
    {
        this.RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("A valid number is required.");

        this.RuleFor(x => x.Letter)
            .Null()
            .When(x => x is null)
            .InclusiveBetween('a', 'z')
            .When(x => x.Letter is not null)
            .WithMessage("A valid letter is required.");
    }
}