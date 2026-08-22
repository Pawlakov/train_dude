// <copyright file="AppendStationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Requests.Lines;

public sealed class AppendStationValidator
    : AbstractValidator<AppendStationCommand>
{
    public AppendStationValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.StationId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid station id is required.");

        this.RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("A valid stream version is required.");
    }
}