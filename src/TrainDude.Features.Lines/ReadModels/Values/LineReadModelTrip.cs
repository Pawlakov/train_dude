// <copyright file="LineReadModelTrip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels.Values;

using System;

public sealed record LineReadModelTrip(Guid Id, int Number);