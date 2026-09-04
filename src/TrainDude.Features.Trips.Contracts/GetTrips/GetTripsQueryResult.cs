// <copyright file="GetTripsQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrips;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripsQueryResult(IReadOnlyList<GetTripsQueryResultItem> Items) : IListQueryResult<GetTripsQueryResultItem>;