// <copyright file="AppendSegmentValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Endpoints.Base;

public sealed class AppendSegmentValidator
    : BaseVersionedDomainValidator<AppendSegmentCommand>
{
    public AppendSegmentValidator()
    {
        this.RuleFor(x => x.SegmentId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid segment id is required.");
    }
}