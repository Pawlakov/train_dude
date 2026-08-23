// <copyright file="BasePolymorphicCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

using System.Text.Json.Serialization;

using TrainDude.Commands.Contracts.Admin;
using TrainDude.Commands.Contracts.Segments;
using TrainDude.Commands.Contracts.Settings;
using TrainDude.Commands.Contracts.Stations;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropCommand), DropCommand.Route)]
[JsonDerivedType(typeof(Lines.CreateCommand), Lines.CreateCommand.Route)]
[JsonDerivedType(typeof(Lines.AppendStationCommand), Lines.AppendStationCommand.Route)]
[JsonDerivedType(typeof(Lines.AssignTripCommand), Lines.AssignTripCommand.Route)]
[JsonDerivedType(typeof(Radii.CreateCommand), Radii.CreateCommand.Route)]
[JsonDerivedType(typeof(Segments.CreateCommand), Segments.CreateCommand.Route)]
[JsonDerivedType(typeof(SetCourseCommand), SetCourseCommand.Route)]
[JsonDerivedType(typeof(Stations.CreateCommand), Stations.CreateCommand.Route)]
[JsonDerivedType(typeof(SetLocationCommand), SetLocationCommand.Route)]
[JsonDerivedType(typeof(AddAxleCommand), AddAxleCommand.Route)]
[JsonDerivedType(typeof(Trips.CreateCommand), Trips.CreateCommand.Route)]
[JsonDerivedType(typeof(SetNameModeCommand), SetNameModeCommand.Route)]
public abstract record BasePolymorphicCommand
{
}