// <copyright file="LineReadModelSegment.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels.Values;

using System;
using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Values;

public sealed record LineReadModelSegment(Guid Id, LineReadModelStation A, LineReadModelStation B, IReadOnlyList<Location> Course);