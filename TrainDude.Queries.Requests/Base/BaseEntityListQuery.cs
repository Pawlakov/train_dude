// <copyright file="BaseEntityListQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using Mediator;

public abstract record class BaseEntityListQuery<TResult>
    : BasePolymorphicQuery, IQuery<TResult>
{
}