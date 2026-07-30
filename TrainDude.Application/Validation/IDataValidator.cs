// <copyright file="IDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Validation;

using FluentValidation;

public interface IDataValidator<T>
    : IValidator<T>
{
}