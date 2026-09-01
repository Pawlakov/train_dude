// <copyright file="GetTripQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrip;

using TrainDude.Features.Base;

public class GetTripQueryResult
    : BaseEntityLookupQueryResult
{
    public required int TripNumber { get; init; }
}