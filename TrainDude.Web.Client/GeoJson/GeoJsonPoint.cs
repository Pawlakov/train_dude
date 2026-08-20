// <copyright file="GeoJsonPoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.GeoJson;

using System.Text.Json.Serialization;

public sealed record class GeoJsonPoint([property: JsonPropertyName("coordinates")] double[] Coordinates) : GeoJsonGeometry();