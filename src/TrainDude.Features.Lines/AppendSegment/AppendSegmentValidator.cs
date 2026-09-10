// <copyright file="AppendSegmentValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;

using FluentValidation;

using TrainDude.Features.Lines.Contracts.AppendSegment;

public sealed class AppendSegmentValidator
    : AbstractValidator<AppendSegmentCommand>
{
    public AppendSegmentValidator()
    {
        this.RuleFor(x => x.SegmentId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid segment id is required.");
    }
}