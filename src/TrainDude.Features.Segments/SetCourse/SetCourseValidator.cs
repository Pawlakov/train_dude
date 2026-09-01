// <copyright file="SetCourseValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using FluentValidation;

using TrainDude.Features.Generic;
using TrainDude.Features.Shared.Validation;

public sealed class SetCourseValidator
    : BaseVersionedDomainValidator<SetCourseCommand, UpdatedResponse>
{
    public SetCourseValidator()
        : base()
    {
        this.RuleFor(x => x.Course)
            .NotNull()
            .NotEmpty()
            .WithMessage("A valid course is required.");
    }
}