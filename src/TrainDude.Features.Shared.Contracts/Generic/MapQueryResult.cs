// <copyright file="MapQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Generic;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Shared.Values;

public sealed record MapQueryResult(IReadOnlyList<Location> StationPoints, IReadOnlyList<IReadOnlyList<Location>> SegmentLineStrings) : IQueryResult;