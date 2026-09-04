// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineQuery(Guid LineId)
    : ILookupQuery<GetLineQueryResult>
{
    public const string TypeRoute = "/line";

    public string Route => TypeRoute;

    public Guid Id => this.LineId;
}