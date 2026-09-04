// <copyright file="IEmptyCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

using TrainDude.Features.Shared.Contracts.Generic;

public interface IEmptyCommand
    : IDomainCommand<EmptyResult>
{
}