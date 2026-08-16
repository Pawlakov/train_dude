// <copyright file="BasePolymorphicCommandResponse.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Base;

using System.Text.Json.Serialization;

using TrainDude.Commands.Requests.Trips;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(CreateTripCommandResult), nameof(CreateTripCommandResult))]
public abstract class BasePolymorphicCommandResponse
{
}