// <copyright file="SetLocationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using FluentValidation;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Validation;
using TrainDude.Features.Stations.Contracts.SetLocation;
using TrainDude.Shared.Values;

public sealed class SetLocationValidator
    : BaseVersionedDomainValidator<SetLocationCommand, UpdatedResult>
{
    public SetLocationValidator()
    {
        this.RuleFor(x => x.Location)
            .NotEqual(default(Location))
            .WithMessage("A valid location id is required.");
    }
}