// <copyright file="SegmentSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

public class SegmentSummaryDTO
{
    public int SegmentId { get; init; }

    public string NameA { get; init; }

    public string NameB { get; init; }

    public double? Length { get; init; }

    public double? Haversine { get; init; }
}