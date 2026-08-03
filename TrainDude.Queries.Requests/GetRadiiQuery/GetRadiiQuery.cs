// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetRadiiQuery;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetRadiiQuery
    : BasePolymorphicQuery, IQuery<GetRadiiQueryResult>
{
}