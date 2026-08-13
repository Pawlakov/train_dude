// <copyright file="BaseEntityListQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Base;

using System.Collections.Generic;

public abstract class BaseEntityListQueryResult<TItem>
    : BasePolymorphicQueryResult
{
    required public IEnumerable<TItem> Items { get; init; }
}