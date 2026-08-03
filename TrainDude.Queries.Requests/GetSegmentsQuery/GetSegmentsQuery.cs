// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetSegmentsQuery;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetSegmentsQuery
    : BasePolymorphicQuery, IQuery<GetSegmentsQueryResult>
{
}