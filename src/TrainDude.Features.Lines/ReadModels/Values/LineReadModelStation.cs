// <copyright file="LineReadModelStation.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels.Values;

using System;

using TrainDude.Features.Shared.Contracts.Values;

public sealed record LineReadModelStation(Guid Id, Location? Location, string Name);