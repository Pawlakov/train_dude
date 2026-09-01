// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.GetSegment;

using System;

using TrainDude.Features.Base;

public sealed record GetSegmentQuery(Guid SegmentId) : BaseEntityLookupQuery<GetSegmentQueryResult>(SegmentId);