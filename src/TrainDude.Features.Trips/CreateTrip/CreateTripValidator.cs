// <copyright file="CreateTripValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.CreateTrip;

using FluentValidation;

public sealed class CreateTripValidator
    : AbstractValidator<CreateTripCommand>
{
    public CreateTripValidator()
    {
        this.RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("A valid number is required.");
    }
}