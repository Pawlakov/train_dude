// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Lines;

using System;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetLineQuery
    : BaseEntityLookupQuery<GetLineQueryResult>
{
}