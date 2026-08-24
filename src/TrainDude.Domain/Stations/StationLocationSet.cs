// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Stations;

using System;

using TrainDude.Domain.Base;
using TrainDude.Shared.Values;

public sealed record class StationLocationSet(Guid Id, DateTime When, Location Location) : BaseAggregateEvent(Id, When);