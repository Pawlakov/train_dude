// <copyright file="GetLinesQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLines;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLinesQuery()
    : IListQuery<GetLinesQueryResult>
{
    public const string TypeRoute = "/api/lines";

    public string Route => TypeRoute;
}