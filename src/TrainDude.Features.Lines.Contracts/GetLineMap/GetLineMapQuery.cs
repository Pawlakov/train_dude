// <copyright file="GetLineMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLineMap;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineMapQuery()
    : IMapQuery
{
    public const string TypeRoute = "/api/lines/{id}/map";

    public string Route => TypeRoute;
}