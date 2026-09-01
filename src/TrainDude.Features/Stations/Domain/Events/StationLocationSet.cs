// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Shared.Values;

public sealed record StationLocationSet(Guid Id, DateTime When, Location Location);