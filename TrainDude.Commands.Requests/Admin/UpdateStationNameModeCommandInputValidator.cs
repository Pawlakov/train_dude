// <copyright file="UpdateStationNameModeCommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using FluentValidation;

using TrainDude.Shared.Validation;

public class UpdateStationNameModeCommandInputValidator
    : AbstractInputValidator<UpdateStationNameModeCommand>
{
    public UpdateStationNameModeCommandInputValidator()
    {
        this.RuleFor(command => command.Mode)
            .IsInEnum()
            .WithMessage("Value out of range.");
    }
}