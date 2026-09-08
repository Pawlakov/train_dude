// <copyright file="GetLineMapQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLineMap;

using System;

using TrainDude.Features.Lines.Contracts.GetLine;
using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineMapQuery(Guid LineId)
    : IMapQuery
{
    public const string TypeRoute = "/api/line/map";

    public string Route => TypeRoute;

    public Guid Id => this.LineId;
}