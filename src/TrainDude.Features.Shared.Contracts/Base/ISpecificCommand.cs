// <copyright file="ISpecificCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

using TrainDude.Features.Shared.Contracts.Generic;

public interface ISpecificCommand
    : ICommand<EmptyResponse>
{
    Guid Id { get; }

    long Version { get; }
}