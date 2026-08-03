// <copyright file="ICommandInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Validation;

using FluentValidation;

public interface ICommandInputValidator<T>
    : IValidator<T>
{
}