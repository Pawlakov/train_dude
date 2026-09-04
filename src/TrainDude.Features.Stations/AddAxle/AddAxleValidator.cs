// <copyright file="AddAxleValidator.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Validation;
using TrainDude.Features.Stations.Contracts.AddAxle;

public sealed class AddAxleValidator
    : BaseVersionedDomainValidator<AddAxleCommand, UpdatedResult>
{
}