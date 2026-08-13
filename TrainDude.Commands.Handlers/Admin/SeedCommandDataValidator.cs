// <copyright file="SeedCommandDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using FluentValidation;

using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.Admin;

public class SeedCommandDataValidator
    : AbstractWriteDataValidator<SeedCommand>
{
    public SeedCommandDataValidator(IHostEnvironment environment)
    {
        this.RuleFor(x => x)
            .Must(_ => environment.IsDevelopment())
            .WithMessage("This operation is only allowed in the Development environment.")
            .WithName("Environment");
    }
}