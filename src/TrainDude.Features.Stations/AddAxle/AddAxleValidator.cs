// <copyright file="AddAxleValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System;

using FluentValidation;

using TrainDude.Features.Stations.Contracts.AddAxle;

public sealed class AddAxleValidator
    : AbstractValidator<AddAxleCommand>
{
    public AddAxleValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid station id is required.");
    }
}