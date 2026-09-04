// <copyright file="IDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IDomainCommand<TResult>
    : IDomainRequest<TResult>
    where TResult : ICommandResult
{
}