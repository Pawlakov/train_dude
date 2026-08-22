// <copyright file="BaseDomainValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Base;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Base;

public abstract class BaseDomainValidator<TCommand>
    : AbstractValidator<TCommand>
    where TCommand : IDomainCommand
{
    public BaseDomainValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid id is required.");
    }
}