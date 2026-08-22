// <copyright file="AssignTripValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Endpoints.Base;

public sealed class AssignTripValidator
    : BaseVersionedDomainValidator<AssignTripCommand>
{
    public AssignTripValidator()
    {
        this.RuleFor(x => x.TripId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid trip id is required.");
    }
}