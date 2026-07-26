// <copyright file="GetSegmentsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using System.Collections.Generic;

using MediatR;

using TrainDude.Network.DTOs;

public class GetSegmentsQuery : IRequest<IEnumerable<SegmentSummaryDTO>>
{
}