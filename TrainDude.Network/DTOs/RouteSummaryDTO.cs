// <copyright file="RouteSummaryDTO.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.DTOs;

public class RouteSummaryDTO
{
    public int Id { get; init; }

    public string? NameA { get; init; }

    public string? NameB { get; init; }

    public double? Length { get; init; }

    public double? Haversine { get; init; }
}