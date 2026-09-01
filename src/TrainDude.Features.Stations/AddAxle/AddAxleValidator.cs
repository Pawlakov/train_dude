// <copyright file="AddAxleValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using TrainDude.Features.Generic;
using TrainDude.Features.Shared.Validation;

public sealed class AddAxleValidator
    : BaseVersionedDomainValidator<AddAxleCommand, UpdatedResponse>
{
}