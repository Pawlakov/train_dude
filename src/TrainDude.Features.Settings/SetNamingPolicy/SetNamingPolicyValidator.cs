// <copyright file="SetNamingPolicyValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.SetNamingPolicy;

using FluentValidation;

using TrainDude.Features.Settings.Contracts.SetNamingPolicy;

public sealed class SetNamingPolicyValidator
    : AbstractValidator<SetNamingPolicyCommand>
{
    public SetNamingPolicyValidator()
    {
        this.RuleFor(x => x.Policy)
            .IsInEnum();
    }
}