// <copyright file="GetTripsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Requests.GetTripsQuery;

using System;

public class GetTripsQueryResultItem
{
    required public Guid TripId { get; init; }

    required public int TripNumber { get; init; }
}