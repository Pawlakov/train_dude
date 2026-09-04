// <copyright file="IListQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IListQuery<TResult>
    : IDomainQuery<TResult>
    where TResult : IQueryResult
{
}