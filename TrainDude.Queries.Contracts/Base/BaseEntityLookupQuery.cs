// <copyright file="BaseEntityLookupQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Base;

using System;

using Mediator;

public abstract record class BaseEntityLookupQuery<TResult>
    : BasePolymorphicQuery, IQuery<TResult>
{
    public Guid Id { get; set; }
}