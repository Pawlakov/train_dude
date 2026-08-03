// <copyright file="AbstractQueryInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Validation;

using FluentValidation;

public abstract class AbstractQueryInputValidator<T>
    : AbstractValidator<T>, IQueryInputValidator<T>
{
}