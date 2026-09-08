// <copyright file="GetSegmentsMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegmentsMap;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentsMapQuery()
    : IMapQuery
{
    public const string TypeRoute = "/api/segments/map";

    public string Route => TypeRoute;
}