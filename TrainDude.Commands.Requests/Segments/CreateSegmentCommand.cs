// <copyright file="CreateSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Segments;

using System;

using TrainDude.Commands.Requests.Base;

public sealed record class CreateSegmentCommand
    : BasePolymorphicCommand
{
    public Guid Id { get; set; }
}