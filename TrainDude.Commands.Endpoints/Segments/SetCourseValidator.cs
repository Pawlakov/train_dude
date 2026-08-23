// <copyright file="SetCourseValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Segments;

using FluentValidation;

using TrainDude.Commands.Contracts.Segments;
using TrainDude.Commands.Endpoints.Base;

public sealed class SetCourseValidator
    : BaseVersionedDomainValidator<SetCourseCommand>
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