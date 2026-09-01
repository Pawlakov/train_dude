// <copyright file="LineStation.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Values;

using System;

using TrainDude.Shared.Values;

public sealed record LineStation(Guid Id, Location? Location, string Name);