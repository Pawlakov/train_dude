// <copyright file="GetTripsQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.GetTrips;

using TrainDude.Features.Base;

public sealed record GetTripsQuery() : BaseEntityListQuery<GetTripsQueryResult>;