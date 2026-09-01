// <copyright file="GetLineQueryResultTripItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.GetLine;

using System;

public class GetLineQueryResultTripItem
{
    public required Guid TripId { get; init; }

    public required int TripNumber { get; init; }
}