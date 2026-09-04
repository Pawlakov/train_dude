// <copyright file="CreateRadiusValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.CreateRadius;

using FluentValidation;

using TrainDude.Features.Radii.Contracts.CreateRadius;

public sealed class CreateRadiusValidator
    : AbstractValidator<CreateRadiusCommand>
{
    public CreateRadiusValidator()
    {
        this.RuleFor(x => x.Speed)
            .GreaterThan(0)
            .WithMessage("A valid speed is required.");

        this.RuleFor(x => x.Minimum)
            .GreaterThan(0)
            .WithMessage("A valid minimum is required.");
    }
}