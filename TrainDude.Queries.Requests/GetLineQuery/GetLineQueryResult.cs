// <copyright file="GetLineQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetLineQuery;

using TrainDude.Queries.Requests.Base;

public class GetLineQueryResult
    : BasePolymorphicQueryResponse
{
    required public string LineDesignation { get; init; }
}