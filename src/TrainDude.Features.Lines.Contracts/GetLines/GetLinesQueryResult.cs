// <copyright file="GetLinesQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLines;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLinesQueryResult(IReadOnlyList<GetLinesQueryResultItem> Items) : IListQueryResult<GetLinesQueryResultItem>;