// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Segments;
using TrainDude.Commands.Endpoints.Base;

public sealed class CreateValidator
    : BaseDomainValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Tracks)
            .GreaterThan(0)
            .WithMessage("A valid number of tracks is required");

        this.RuleFor(x => x.AId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid A id is required.");

        this.RuleFor(x => x.AAxle)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A valid A axle is required.");

        this.RuleFor(x => x.BId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid B id is required.");

        this.RuleFor(x => x.BAxle)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A valid B axle is required.");
    }
}