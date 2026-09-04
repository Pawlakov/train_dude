// <copyright file="ICreateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

using TrainDude.Features.Shared.Contracts.Generic;

public interface ICreateCommand
    : IDomainCommand<CreatedResult>
{
}