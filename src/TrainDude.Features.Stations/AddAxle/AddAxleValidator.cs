// <copyright file="AddAxleValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using FluentValidation;

using TrainDude.Features.Stations.Contracts.AddAxle;

public sealed class AddAxleValidator
    : AbstractValidator<AddAxleCommand>
{
}