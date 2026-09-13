// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineQuery(Guid Id)
    : ILookupQuery<GetLineQueryResponse>
{
    public const string TypeRoute = "/api/lines/{id}";

    public static string Route => TypeRoute;
}