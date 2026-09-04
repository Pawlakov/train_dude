// <copyright file="AssignTripValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using System;

using FluentValidation;

using TrainDude.Features.Lines.Contracts.AssignTrip;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Validation;

public sealed class AssignTripValidator
    : BaseVersionedDomainValidator<AssignTripCommand, UpdatedResult>
{
    public AssignTripValidator()
    {
        this.RuleFor(x => x.TripId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid trip id is required.");
    }
}