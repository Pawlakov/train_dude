// <copyright file="GetSegmentGeoJsonQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Application.Requests.GetSegmentGeoJsonQuery;

using TrainDude.Application.Requests.Base;

public class GetSegmentGeoJsonQueryResult
    : BasePolymorphicResponse
{
    public string GeoJson { get; set; }
}