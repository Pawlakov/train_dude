// <copyright file="GetRadiiQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Queries;

using System;
using System.Collections.Generic;
using System.Text;

using MediatR;

using TrainDude.Network.DTOs;

public class GetRadiiQuery : IRequest<IEnumerable<RadiusSummaryDTO>>
{
}