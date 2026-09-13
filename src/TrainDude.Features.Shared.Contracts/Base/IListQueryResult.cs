// <copyright file="IListQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System.Collections.Generic;

public interface IListQueryResult<TItem>
    : IResult
{
    IReadOnlyList<TItem> Items { get; }
}