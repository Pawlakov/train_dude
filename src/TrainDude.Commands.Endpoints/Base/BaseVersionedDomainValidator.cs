// <copyright file="VersionedDomainValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Base;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Base;

public abstract class BaseVersionedDomainValidator<TCommand>
    : BaseDomainValidator<TCommand>
    where TCommand : IVersionedDomainCommand
{
    public BaseVersionedDomainValidator()
    {
        this.RuleFor(x => x.Version)
            .GreaterThan(0)
            .WithMessage("A valid stream version is required.");
    }
}