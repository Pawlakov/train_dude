// <copyright file="BasePolymorphicCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Commands.Requests.Admin;
using TrainDude.Commands.Requests.Trips;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropCommand), nameof(DropCommand))]
[JsonDerivedType(typeof(UpdateStationNameModeCommand), nameof(UpdateStationNameModeCommand))]
[JsonDerivedType(typeof(CreateTripCommand), nameof(CreateTripCommand))]
public abstract record class BasePolymorphicCommand
{
}