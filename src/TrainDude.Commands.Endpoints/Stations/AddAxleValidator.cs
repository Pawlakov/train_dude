// <copyright file="AddAxleValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Endpoints.Stations;

using System;

using FluentValidation;

using TrainDude.Commands.Contracts.Stations;
using TrainDude.Commands.Endpoints.Base;

public sealed class AddAxleValidator
    : BaseVersionedDomainValidator<AddAxleCommand>
{
    public AddAxleValidator()
        : base()
    {
    }
}