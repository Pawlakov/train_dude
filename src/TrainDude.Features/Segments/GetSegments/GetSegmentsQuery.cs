// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegments;

using TrainDude.Features.Base;

public sealed record GetSegmentsQuery() : BaseEntityListQuery<GetSegmentsQueryResult>;