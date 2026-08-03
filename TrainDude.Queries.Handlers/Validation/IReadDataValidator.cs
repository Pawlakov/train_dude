// <copyright file="IReadDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Validation;

using FluentValidation;

public interface IReadDataValidator<T>
    : IValidator<T>
{
}