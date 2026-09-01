// <copyright file="CreateSegmentValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;

using FluentValidation;

public sealed class CreateSegmentValidator
    : AbstractValidator<CreateSegmentCommand>
{
    public CreateSegmentValidator()
    {
        this.RuleFor(x => x.Tracks)
            .GreaterThan(0)
            .WithMessage("A valid number of tracks is required");

        this.RuleFor(x => x.A.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid A id is required.");

        this.RuleFor(x => x.A.Axle)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A valid A axle is required.");

        this.RuleFor(x => x.B.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid B id is required.");

        this.RuleFor(x => x.B.Axle)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A valid B axle is required.");
    }
}