// <copyright file="AbstractReadDataValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Handlers.Validation;

using FluentValidation;

public abstract class AbstractReadDataValidator<T>
    : AbstractValidator<T>, IReadDataValidator<T>
{
}