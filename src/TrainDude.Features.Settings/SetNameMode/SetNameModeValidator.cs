// <copyright file="SetNameModeValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.SetNameMode;

using FluentValidation;

public sealed class SetNameModeValidator
    : AbstractValidator<SetNameModeCommand>
{
    public SetNameModeValidator()
    {
        this.RuleFor(x => x.Mode)
            .IsInEnum();
    }
}