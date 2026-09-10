// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.GetSegments;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetSegmentsQuery()
    : IListQuery<GetSegmentsQueryResult>
{
    public const string TypeRoute = "/api/segments";

    public static string Route => TypeRoute;
}