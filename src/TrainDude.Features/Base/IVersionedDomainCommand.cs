// <copyright file="IVersionedDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System;

public interface IVersionedDomainCommand<TResult>
    : IDomainCommand<TResult>
    where TResult : BaseCommandResponse
{
    Guid Id { get; }

    long Version { get; }
}