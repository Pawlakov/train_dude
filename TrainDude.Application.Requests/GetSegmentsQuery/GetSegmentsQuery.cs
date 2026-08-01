// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentsQuery;

using Mediator;

using TrainDude.Application.Requests.Base;

public sealed record class GetSegmentsQuery
    : BasePolymorphicQuery, IQuery<GetSegmentsQueryResult>
{
}