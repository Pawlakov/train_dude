// <copyright file="GetSegmentMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegmentMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentMapQuery()
    : IMapQuery
{
    public const string TypeRoute = "/api/segments/{id}/map";

    public string Route => TypeRoute;
}