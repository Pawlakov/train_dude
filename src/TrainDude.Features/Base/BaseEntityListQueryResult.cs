// <copyright file="BaseEntityListQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System.Collections.Generic;

public abstract class BaseEntityListQueryResult<TItem>
{
    public required IEnumerable<TItem> Items { get; init; }
}