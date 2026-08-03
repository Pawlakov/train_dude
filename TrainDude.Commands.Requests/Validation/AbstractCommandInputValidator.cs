// <copyright file="AbstractCommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Validation;

using FluentValidation;

public abstract class AbstractCommandInputValidator<T>
    : AbstractValidator<T>, ICommandInputValidator<T>
{
}