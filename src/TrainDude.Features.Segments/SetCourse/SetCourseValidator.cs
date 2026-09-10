// <copyright file="SetCourseValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.SetCourse;

using FluentValidation;

using TrainDude.Features.Segments.Contracts.SetCourse;

public sealed class SetCourseValidator
    : AbstractValidator<SetCourseCommand>
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