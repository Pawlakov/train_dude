// <copyright file="UpdateStationNameModeCommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin;

using FluentValidation;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Web.Client.Validation;

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