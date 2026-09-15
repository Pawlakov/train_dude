// <copyright file="GetTripsQueryResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrips;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetTripsQueryResponse(IReadOnlyList<GetTripsQueryResultItem> Items) : IListQueryResponse<GetTripsQueryResultItem>;