// <copyright file="GetSegmentsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetSegmentsQuery;

using TrainDude.Application.Requests.Base;

public class GetSegmentsQueryResult
    : BasePolymorphicResponse
{
    public IEnumerable<GetSegmentsQueryResultItem> Items { get; set; }
}