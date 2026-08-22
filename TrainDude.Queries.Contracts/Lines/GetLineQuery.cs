// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Lines;

using System;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetLineQuery
    : BaseEntityLookupQuery<GetLineQueryResult>
{
}