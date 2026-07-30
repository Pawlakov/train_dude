// <copyright file="GetSegmentsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentsQuery;

public class GetSegmentsQueryResult
{
    public IEnumerable<GetSegmentsQueryResultItem> Items { get; set; }
}