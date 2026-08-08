// <copyright file="GetLineQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Queries.Requests.GetLineQuery;

using System;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetLineQuery
    : BasePolymorphicQuery, IQuery<GetLineQueryResult>
{
    public Guid LineId { get; set; }
}