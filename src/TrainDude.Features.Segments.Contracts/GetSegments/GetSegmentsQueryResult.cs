// <copyright file="GetSegmentsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegments;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentsQueryResult(IReadOnlyList<GetSegmentsQueryResultItem> Items) : IListQueryResult<GetSegmentsQueryResultItem>;