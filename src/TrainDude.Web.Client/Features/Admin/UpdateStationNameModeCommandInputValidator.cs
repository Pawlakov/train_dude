// <copyright file="UpdateStationNameModeCommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin;

using FluentValidation;

using TrainDude.Features.Settings;
using TrainDude.Features.Settings.SetNameMode;
using TrainDude.Web.Client.Validation;

public class UpdateStationNameModeCommandInputValidator
    : AbstractInputValidator<SetNameModeCommand>
{
    public UpdateStationNameModeCommandInputValidator()
    {
        this.RuleFor(command => command.Mode)
            .IsInEnum()
            .WithMessage("Value out of range.");
    }
}