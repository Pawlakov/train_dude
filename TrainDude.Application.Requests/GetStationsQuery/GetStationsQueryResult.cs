// <copyright file="GetStationsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetStationsQuery;

using TrainDude.Application.Requests.Base;

public class GetStationsQueryResult
    : BaseClientResponse
{
    public IEnumerable<GetStationsQueryResultItem> Items { get; set; }
}