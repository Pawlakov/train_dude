// <copyright file="GeoJsonGeometry.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Web.Client.GeoJson;

using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(GeoJsonLineString), "LineString")]
[JsonDerivedType(typeof(GeoJsonPoint), "Point")]
public abstract record class GeoJsonGeometry();