// <copyright file="GetStationsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetStationsQuery;

public class GetStationsQueryResult
{
    public IEnumerable<GetStationsQueryResultItem> Items { get; set; }
}