// <copyright file="SetNameModeValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Settings;

using FluentValidation;

using TrainDude.Commands.Contracts.Admin;
using TrainDude.Commands.Contracts.Settings;

public sealed class SetNameModeValidator
    : AbstractValidator<SetNameModeCommand>
{
    public SetNameModeValidator()
    {
        this.RuleFor(x => x.Mode)
            .IsInEnum();
    }
}