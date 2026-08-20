// <copyright file="GeoJsonFeature.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.GeoJson;

using System.Text.Json.Serialization;

public sealed record class GeoJsonFeature([property: JsonPropertyName("type")] string Type, [property: JsonPropertyName("geometry")] GeoJsonGeometry Geometry, [property: JsonPropertyName("properties")] object? Properties = null);