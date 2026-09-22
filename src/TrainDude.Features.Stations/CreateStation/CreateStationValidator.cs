// <copyright file="CreateStationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.CreateStation;

using System.Linq;

using FluentValidation;

using TrainDude.Features.Shared.Extensions;
using TrainDude.Features.Stations.Contracts.CreateStation;

public sealed class CreateStationValidator
    : AbstractValidator<CreateStationCommand>
{
    public CreateStationValidator()
    {
        this.RuleFor(x => x.NameGerman)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .MustBeValidStationName();

        this.RuleFor(x => x.NameGermanNew)
            .Cascade(CascadeMode.Stop)
            .MustBeValidStationName()
            .When(x => x.NameGermanNew != null);

        this.RuleFor(x => x.NamePolish)
            .Cascade(CascadeMode.Stop)
            .MustBeValidStationName()
            .When(x => x.NamePolish != null);

        this.RuleFor(x => x.NameRussian)
            .Cascade(CascadeMode.Stop)
            .MustBeValidStationName()
            .When(x => x.NameRussian != null);

        this.RuleFor(x => x.NamePolish)
            .Empty().WithMessage("You can't supply both polish and russian names together.")
            .When(x => x.NameRussian != null);

        this.RuleFor(x => x.NameRussian)
            .Empty().WithMessage("You can't supply both polish and russian names together.")
            .When(x => x.NamePolish != null);
    }
}