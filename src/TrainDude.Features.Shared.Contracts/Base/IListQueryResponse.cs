// <copyright file="IListQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System.Collections.Generic;

public interface IListQueryResponse<TItem>
    : IResponse
{
    IReadOnlyList<TItem> Items { get; }
}