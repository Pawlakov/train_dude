// <copyright file="BaseVersionedDomainValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Validation;

using System;

using FluentValidation;

using TrainDude.Features.Base;

public abstract class BaseVersionedDomainValidator<TCommand, TResult>
    : AbstractValidator<TCommand>
    where TCommand : IVersionedDomainCommand<TResult>
    where TResult : BaseCommandResponse
{
    public BaseVersionedDomainValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");

        this.RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("A valid stream version is required.");
    }
}