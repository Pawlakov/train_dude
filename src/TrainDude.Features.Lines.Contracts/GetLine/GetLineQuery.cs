// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineQuery()
    : ILookupQuery<GetLineQueryResult>
{
    public const string TypeRoute = "/api/lines/{id}";

    public string Route => TypeRoute;
}