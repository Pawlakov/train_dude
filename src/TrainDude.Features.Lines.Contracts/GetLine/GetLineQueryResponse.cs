// <copyright file="GetLineQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.GetLine;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record GetLineQueryResponse(string LineDesignation, IReadOnlyList<GetLineQueryResultStationItem> Stations, IReadOnlyList<GetLineQueryResultTripItem> Trips)
    : ILookupQueryResponse;