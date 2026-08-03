// <copyright file="AbstractWriteDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Validation;

using FluentValidation;

public abstract class AbstractWriteDataValidator<T>
    : AbstractValidator<T>, IWriteDataValidator<T>
{
}