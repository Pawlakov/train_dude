// <copyright file="GetStationQueryResult.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Contracts.GetStation;

using System.Collections.Generic;

using TrainDude.Features.Shared.Contracts.Base;
using TrainDude.Features.Shared.Contracts.Values;

public sealed record GetStationQueryResult(string Name, Location? Location, int AxleCount) : ILookupQueryResult;