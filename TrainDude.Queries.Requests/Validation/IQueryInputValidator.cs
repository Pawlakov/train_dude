// <copyright file="IQueryInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Validation;

using FluentValidation;

public interface IQueryInputValidator<T>
    : IValidator<T>
{
}