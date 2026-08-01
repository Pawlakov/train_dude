// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetRadiiQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

public sealed record class GetRadiiQuery
    : BasePolymorphicQuery, IQuery<GetRadiiQueryResult>
{
}