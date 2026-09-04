// <copyright file="UpdateStationNameModeCommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Features.Admin;

using FluentValidation;

using TrainDude.Features.Settings.Contracts.SetNamingPolicy;
using TrainDude.Web.Client.Validation;

public class UpdateStationNameModeCommandInputValidator
    : AbstractInputValidator<SetNamingPolicyCommand>
{
    public UpdateStationNameModeCommandInputValidator()
    {
        this.RuleFor(command => command.Policy)
            .IsInEnum()
            .WithMessage("Value out of range.");
    }
}