// <copyright file="CreateStationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using FluentValidation;

using TrainDude.Features.Stations.Contracts.CreateStation;

public sealed class CreateStationValidator
    : AbstractValidator<CreateStationCommand>
{
    public CreateStationValidator()
    {
        this.RuleFor(x => x.NameGerman)
            .NotEmpty()
            .WithMessage("A valid name is required.")
            .MaximumLength(200);

        this.RuleFor(x => x.NamePolish)
            .Empty()
            .When(x => !string.IsNullOrEmpty(x.NameRussian))
            .WithMessage("You can't supply both polish and russian names together.");

        this.RuleFor(x => x.NameRussian)
            .Empty()
            .When(x => !string.IsNullOrEmpty(x.NamePolish))
            .WithMessage("You can't supply both polish and russian names together.");
    }
}