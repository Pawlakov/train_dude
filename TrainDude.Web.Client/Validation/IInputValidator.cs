// <copyright file="IInputValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Validation;

using FluentValidation;

public interface IInputValidator<T>
    : IValidator<T>
{
}