// <copyright file="GetTripQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrip;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripQueryResponse(int TripNumber) : ILookupQueryResponse;