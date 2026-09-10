// <copyright file="IQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IQuery<TResult>
    : IDomainRequest
    where TResult : IQueryResult
{
}