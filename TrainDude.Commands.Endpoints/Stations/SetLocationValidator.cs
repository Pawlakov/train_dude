// <copyright file="SetLocationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using TrainDude.Commands.Requests.Stations;
using TrainDude.Integration.Values;

public class SetLocationValidator
    : AbstractValidator<SetLocationCommand>
{
    public SetLocationValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.Location)
            .NotEqual(default(Location))
            .WithMessage("A valid location id is required.");

        this.RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("A valid stream version is required.");
    }
}