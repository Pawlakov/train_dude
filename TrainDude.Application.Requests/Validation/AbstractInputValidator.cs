// <copyright file="AbstractInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.Validation;

using FluentValidation;

public abstract class AbstractInputValidator<T>
    : AbstractValidator<T>, IInputValidator<T>
{
}