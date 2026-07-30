// <copyright file="AbstractDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Validation;

using FluentValidation;

public abstract class AbstractDataValidator<T>
    : AbstractValidator<T>, IDataValidator<T>
{
}