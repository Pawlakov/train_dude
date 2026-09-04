// <copyright file="IDomainQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IDomainQuery<TResult>
    : IDomainRequest<TResult>
    where TResult : IQueryResult
{
}