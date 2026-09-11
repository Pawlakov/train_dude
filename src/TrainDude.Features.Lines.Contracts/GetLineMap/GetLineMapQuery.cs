// <copyright file="GetLineMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLineMap;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Generic;

public sealed record GetLineMapQuery()
    : IMapQuery, ISpecificQuery<MapQueryResult>
{
    public const string TypeRoute = "/api/lines/{id}/map";

    public static string Route => TypeRoute;
}