// <copyright file="GetTripsQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Queries.Contracts.Trips;

using System;

public class GetTripsQueryResultItem
{
    public required Guid TripId { get; init; }

    public required int TripNumber { get; init; }
}