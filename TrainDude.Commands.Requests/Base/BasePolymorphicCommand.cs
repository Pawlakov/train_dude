// <copyright file="BasePolymorphicCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Commands.Requests.SeedCommand;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(SeedCommand), nameof(SeedCommand))]
public abstract record class BasePolymorphicCommand
    : IMessage
{
}