// <copyright file="CreateStationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using FluentValidation;

public sealed class CreateStationValidator
    : AbstractValidator<CreateStationCommand>
{
    public CreateStationValidator()
    {
        this.RuleFor(x => x.NameGerman)
            .NotEmpty()
            .WithMessage("A valid name is required.")
            .MaximumLength(200);
    }
}