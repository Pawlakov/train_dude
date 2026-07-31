// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetStationsQuery;

using MediatR;

using TrainDude.Application.Requests.Base;

/// <summary>
/// A query which returns all stations.
/// </summary>
public class GetStationsQuery
    : BaseClientRequest, IRequest<GetStationsQueryResult>
{
}