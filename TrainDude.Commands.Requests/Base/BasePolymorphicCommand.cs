// <copyright file="BasePolymorphicCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Commands.Requests.Lines;
using TrainDude.Commands.Requests.Radii;
using TrainDude.Commands.Requests.Segments;
using TrainDude.Commands.Requests.Stations;
using TrainDude.Commands.Requests.Trips;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropCommand), nameof(DropCommand))]
[JsonDerivedType(typeof(UpdateStationNameModeCommand), nameof(UpdateStationNameModeCommand))]
[JsonDerivedType(typeof(CreateStationCommand), nameof(CreateStationCommand))]
[JsonDerivedType(typeof(SetLocationCommand), nameof(SetLocationCommand))]
[JsonDerivedType(typeof(AddAxleCommand), nameof(AddAxleCommand))]
[JsonDerivedType(typeof(CreateTripCommand), nameof(CreateTripCommand))]
[JsonDerivedType(typeof(CreateLineCommand), nameof(CreateLineCommand))]
[JsonDerivedType(typeof(AssignTripCommand), nameof(AssignTripCommand))]
[JsonDerivedType(typeof(AppendStationCommand), nameof(AppendStationCommand))]
[JsonDerivedType(typeof(CreateRadiusCommand), nameof(CreateRadiusCommand))]
[JsonDerivedType(typeof(CreateSegmentCommand), nameof(CreateSegmentCommand))]
public abstract record class BasePolymorphicCommand
{
}