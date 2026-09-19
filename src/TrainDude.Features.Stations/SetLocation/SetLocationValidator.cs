// <copyright file="SetLocationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System;

using FluentValidation;

using TrainDude.Features.Shared.Contracts.Values;
using TrainDude.Features.Stations.Contracts.SetLocation;

public sealed class SetLocationValidator
    : AbstractValidator<SetLocationCommand>
{
    public SetLocationValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid station id is required.");

        this.RuleFor(x => x.Location)
            .NotEqual(default(Location))
            .WithMessage("A valid location is required.");
    }
}