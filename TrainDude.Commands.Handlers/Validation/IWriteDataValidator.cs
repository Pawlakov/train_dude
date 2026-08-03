// <copyright file="IWriteDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Handlers.Validation;

using FluentValidation;

public interface IWriteDataValidator<T>
    : IValidator<T>
{
}