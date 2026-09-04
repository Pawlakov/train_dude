// <copyright file="GetRadiiQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Contracts.GetRadii;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetRadiiQueryResult(IReadOnlyList<GetRadiiQueryResultItem> Items) : IListQueryResult<GetRadiiQueryResultItem>;