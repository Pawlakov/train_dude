// <copyright file="GeoJsonFeatureCollection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.GeoJson;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public sealed record class GeoJsonFeatureCollection([property: JsonPropertyName("type")] string Type, [property: JsonPropertyName("features")] IReadOnlyList<GeoJsonFeature> Features);