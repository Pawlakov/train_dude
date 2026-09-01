// <copyright file="GetTripQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrip;

using System;

using TrainDude.Features.Base;

public sealed record GetTripQuery(Guid TripId) : BaseEntityLookupQuery<GetTripQueryResult>(TripId);