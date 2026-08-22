// <copyright file="AssignTripValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Requests.Lines;

public class AssignTripValidator
    : AbstractValidator<AssignTripCommand>
{
    public AssignTripValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.TripId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid trip id is required.");

        this.RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("A valid stream version is required.");
    }
}