// <copyright file="LineTrip.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Values;

using System;

public sealed record LineTrip(Guid Id, int Number);