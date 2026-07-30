// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.GetStationsQuery;

using MediatR;

/// <summary>
/// A query which returns all stations.
/// </summary>
public class GetStationsQuery : IRequest<GetStationsQueryResult>
{
}