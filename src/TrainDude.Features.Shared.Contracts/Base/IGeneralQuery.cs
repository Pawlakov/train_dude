// <copyright file="IGeneralQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IGeneralQuery<TResult>
    : IQuery<TResult>
    where TResult : IQueryResult
{
}