// <copyright file="GeoJsonLineString.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services.GeoJson;

using System.Text.Json.Serialization;

public sealed record GeoJsonLineString([property: JsonPropertyName("coordinates")] double[][] Coordinates) : GeoJsonGeometry();