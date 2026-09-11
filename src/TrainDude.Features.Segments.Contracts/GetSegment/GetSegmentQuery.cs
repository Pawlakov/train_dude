// <copyright file="GetSegmentQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegment;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentQuery()
    : ILookupQuery<GetSegmentQueryResult>
{
    public const string TypeRoute = "/api/segments/{id}";

    public static string Route => TypeRoute;
}