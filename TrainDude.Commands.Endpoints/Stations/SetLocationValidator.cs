// <copyright file="SetLocationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Stations;
using TrainDude.Commands.Endpoints.Base;
using TrainDude.Shared.Values;

public sealed class SetLocationValidator
    : BaseVersionedDomainValidator<SetLocationCommand>
{
    public SetLocationValidator()
    : base()
    {
        this.RuleFor(x => x.Location)
            .NotEqual(default(Location))
            .WithMessage("A valid location id is required.");
    }
}