// <copyright file="GetRadiusQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Radii.Contracts.GetRadius;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetRadiusQuery()
    : IListQuery<GetRadiusQueryResult>
{
    public static string Route => "/api/radii/{id}";
}