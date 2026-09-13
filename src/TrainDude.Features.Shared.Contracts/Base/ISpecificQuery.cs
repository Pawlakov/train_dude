// <copyright file="ISpecificQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

public interface ISpecificQuery<TResult>
    : IQuery<TResult>
    where TResult : IQueryResult
{
    Guid Id { get; }
}