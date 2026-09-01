// <copyright file="GetLinesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLines;

using TrainDude.Features.Base;

public sealed record GetLinesQuery() : BaseEntityListQuery<GetLinesQueryResult>;