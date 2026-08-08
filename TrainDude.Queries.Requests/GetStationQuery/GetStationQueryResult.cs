// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetStationQuery;

using TrainDude.Queries.Requests.Base;

public class GetStationQueryResult
    : BasePolymorphicQueryResponse
{
    required public string Name { get; init; }
}