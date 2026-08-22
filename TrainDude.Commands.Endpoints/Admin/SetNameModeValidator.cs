// <copyright file="SetNameModeValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Commands.Endpoints.Admin;

using FluentValidation;

using TrainDude.Commands.Requests.Admin;

public sealed class SetNameModeValidator
    : AbstractValidator<SetNameModeCommand>
{
    public SetNameModeValidator()
    {
        this.RuleFor(x => x.Mode)
            .IsInEnum();
    }
}