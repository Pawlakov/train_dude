// <copyright file="GetRadiiQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetRadiiQuery;

using TrainDude.Application.Requests.Base;

public class GetRadiiQueryResult
    : BasePolymorphicResponse
{
    public IEnumerable<GetRadiiQueryResultItem> Items { get; set; }
}