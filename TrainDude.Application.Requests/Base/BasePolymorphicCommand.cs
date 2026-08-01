// <copyright file="BasePolymorphicCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Application.Requests.Base;

using System.Text.Json.Serialization;

using Mediator;

using TrainDude.Application.Requests.DropAndSeedCommand;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(DropAndSeedCommand), nameof(DropAndSeedCommand))]
public abstract record class BasePolymorphicCommand
    : IMessage
{
}