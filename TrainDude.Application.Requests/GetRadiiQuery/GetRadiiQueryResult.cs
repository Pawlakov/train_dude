// <copyright file="GetRadiiQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetRadiiQuery;

public class GetRadiiQueryResult
{
    public IEnumerable<GetRadiiQueryResultItem> Items { get; set; }
}