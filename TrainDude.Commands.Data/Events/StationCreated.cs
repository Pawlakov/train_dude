// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Events;

using System;

public record class StationCreated(Guid StationId, string NameGerman);