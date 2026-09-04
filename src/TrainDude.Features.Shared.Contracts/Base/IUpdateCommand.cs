// <copyright file="IUpdateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

using TrainDude.Features.Shared.Contracts.Generic;

public interface IUpdateCommand
    : IVersionedDomainCommand<UpdatedResult>
{
    Guid Id { get; }

    long Version { get; }
}