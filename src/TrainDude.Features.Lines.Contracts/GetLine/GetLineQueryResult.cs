// <copyright file="GetLineQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record GetLineQueryResult(string LineDesignation, IReadOnlyList<GetLineQueryResultStationItem> Stations, IReadOnlyList<GetLineQueryResultTripItem> Trips)
    : ILookupQueryResult;