// <copyright file="GeoJsonFeatureCollection.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.Services.GeoJson;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public sealed record GeoJsonFeatureCollection([property: JsonPropertyName("type")] string Type, [property: JsonPropertyName("features")] IReadOnlyList<GeoJsonFeature> Features);