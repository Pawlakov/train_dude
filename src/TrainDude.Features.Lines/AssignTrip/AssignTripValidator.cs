// <copyright file="AssignTripValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using System;

using FluentValidation;

using TrainDude.Features.Lines.Contracts.AssignTrip;

public sealed class AssignTripValidator
    : AbstractValidator<AssignTripCommand>
{
    public AssignTripValidator()
    {
        this.RuleFor(x => x.TripId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid trip id is required.");
    }
}