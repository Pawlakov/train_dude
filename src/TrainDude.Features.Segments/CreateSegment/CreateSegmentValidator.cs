// <copyright file="CreateSegmentValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;

using FluentValidation;

using TrainDude.Features.Segments.Contracts.CreateSegment;

public sealed class CreateSegmentValidator
    : AbstractValidator<CreateSegmentCommand>
{
    public CreateSegmentValidator()
    {
        this.RuleFor(x => x.NominalLength)
            .GreaterThan(0)
            .WithMessage("A valid nominal length is required");

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

        this.RuleFor(x => x.BId)
            .NotEqual(x => x.AId)
            .WithMessage("A and B ids can't be the same.");
    }
}