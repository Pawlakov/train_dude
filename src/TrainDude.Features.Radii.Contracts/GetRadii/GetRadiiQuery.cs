// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Contracts.GetRadii;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetRadiiQuery()
    : IListQuery<GetRadiiQueryResult>
{
    public const string TypeRoute = "/api/radii";

    public static string Route => TypeRoute;
}