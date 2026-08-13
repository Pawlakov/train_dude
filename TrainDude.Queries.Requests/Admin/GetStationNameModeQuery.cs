// <copyright file="GetStationNameModeQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Admin;

using Mediator;

using TrainDude.Queries.Requests.Base;

public sealed record class GetStationNameModeQuery
    : BasePolymorphicQuery, IQuery<GetStationNameModeQueryResult>
{
}