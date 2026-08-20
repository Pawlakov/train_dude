// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Stations;

using System.Collections.Generic;

using TrainDude.Integration.Values;
using TrainDude.Queries.Requests.Base;
using TrainDude.Queries.Requests.Network;

public class GetStationQueryResult
    : BaseEntityLookupQueryResult
{
    required public string Name { get; init; }
}