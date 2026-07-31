// <copyright file="GetNetworkGeoJsonQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Application.Requests.GetNetworkGeoJsonQuery;

using TrainDude.Application.Requests.Base;

public class GetNetworkGeoJsonQueryResult
    : BaseClientResponse
{
    public string GeoJson { get; set; }
}