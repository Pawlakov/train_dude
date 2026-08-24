// <copyright file="CreateValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Endpoints.Base;

public sealed class CreateValidator
    : BaseDomainValidator<CreateCommand>
{
    public CreateValidator()
    {
        this.RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("A valid number is required.");

        this.RuleFor(x => x.Letter)
            .Null()
            .When(x => x is null)
            .InclusiveBetween('a', 'z')
            .When(x => x.Letter is not null)
            .WithMessage("A valid letter is required.");
    }
}