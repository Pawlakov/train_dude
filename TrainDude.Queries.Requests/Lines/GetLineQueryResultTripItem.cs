// <copyright file="GetLineQueryResultTripItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.Lines;

using System;

public class GetLineQueryResultTripItem
{
    required public Guid TripId { get; init; }

    required public int TripNumber { get; init; }
}