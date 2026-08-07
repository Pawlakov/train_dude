// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Events;

using System;

using TrainDude.Shared.Values;

public record class StationLocationSet(Location Location);