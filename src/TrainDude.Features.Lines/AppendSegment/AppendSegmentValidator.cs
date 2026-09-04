// <copyright file="AppendSegmentValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;

using FluentValidation;

using TrainDude.Features.Lines.Contracts.AppendSegment;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Validation;

public sealed class AppendSegmentValidator
    : BaseVersionedDomainValidator<AppendSegmentCommand, UpdatedResult>
{
    public AppendSegmentValidator()
    {
        this.RuleFor(x => x.SegmentId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid segment id is required.");
    }
}