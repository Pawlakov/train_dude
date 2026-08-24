// <copyright file="GetStationNameModeQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Admin;

using Mediator;

using TrainDude.Queries.Contracts.Base;

public sealed record class GetStationNameModeQuery
    : BasePolymorphicQuery, IQuery<GetStationNameModeQueryResult>
{
}