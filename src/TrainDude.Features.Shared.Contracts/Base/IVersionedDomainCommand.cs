// <copyright file="IVersionedDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

public interface IVersionedDomainCommand<TResult>
    : IDomainCommand<TResult>
    where TResult : ICommandResult
{
    Guid Id { get; }

    long Version { get; }
}