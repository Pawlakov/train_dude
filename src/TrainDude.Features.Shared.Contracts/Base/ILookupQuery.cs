// <copyright file="ILookupQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System;

public interface ILookupQuery<TResponse>
    : ISpecificQuery<TResponse>
    where TResponse : IResponse
{
}