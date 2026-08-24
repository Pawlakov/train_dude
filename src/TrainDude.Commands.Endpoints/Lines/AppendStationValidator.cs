// <copyright file="AppendStationValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Lines;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Lines;
using TrainDude.Commands.Endpoints.Base;

public sealed class AppendStationValidator
    : BaseVersionedDomainValidator<AppendStationCommand>
{
    public AppendStationValidator()
    {
        this.RuleFor(x => x.StationId)
            .NotEqual(Guid.Empty)
            .WithMessage("A valid station id is required.");
    }
}