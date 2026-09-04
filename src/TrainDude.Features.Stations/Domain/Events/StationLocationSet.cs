// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Stations.Domain.Events;

using System;

using TrainDude.Shared.Values;

public sealed record StationLocationSet(Guid Id, DateTime When, Location Location);