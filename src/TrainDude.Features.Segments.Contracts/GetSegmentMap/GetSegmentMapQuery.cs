// <copyright file="GetSegmentMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegmentMap;

using System;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;

public sealed record GetSegmentMapQuery()
    : IMapQuery, ISpecificQuery<MapQueryResult>
{
    public const string TypeRoute = "/api/segments/{0}/map";

    public static string Route => TypeRoute;
}