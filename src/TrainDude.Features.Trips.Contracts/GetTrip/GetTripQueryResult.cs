// <copyright file="GetTripQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.Contracts.GetTrip;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record GetTripQueryResult(int TripNumber, IReadOnlyList<Location> StationPoints, IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings) : ILookupQueryResult;