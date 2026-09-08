// <copyright file="GetSegmentMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegmentMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentMapQuery(Guid SegmentId)
    : IMapQuery
{
    public const string TypeRoute = "/api/segment/map";

    public string Route => TypeRoute;

    public Guid Id => this.SegmentId;
}