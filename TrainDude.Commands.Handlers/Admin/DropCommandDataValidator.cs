// <copyright file="DropCommandDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Admin;

using FluentValidation;

using Microsoft.Extensions.Hosting;

using TrainDude.Commands.Handlers.Validation;
using TrainDude.Commands.Requests.Admin;

public class DropCommandDataValidator
    : AbstractWriteDataValidator<DropCommand>
{
    public DropCommandDataValidator(IHostEnvironment environment)
    {
        this.RuleFor(x => x)
            .Must(_ => environment.IsDevelopment())
            .WithMessage("This operation is only allowed in the Development environment.")
            .WithName("Environment");
    }
}