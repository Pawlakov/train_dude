// <copyright file="ISpecificQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

public interface ISpecificQuery<TResponse>
    : IQuery<TResponse>
    where TResponse : IResponse
{
    Guid Id { get; }
}