// <copyright file="GetStationsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using System.Collections.Generic;

using MediatR;
using TrainDude.Network.DTOs;

/// <summary>
/// A query which returns all stations.
/// </summary>
public class GetStationsQuery : IRequest<IEnumerable<StationSummaryDTO>>
{
}