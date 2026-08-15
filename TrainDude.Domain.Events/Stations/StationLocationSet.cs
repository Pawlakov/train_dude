// <copyright file="StationLocationSet.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Events.Stations;

using System;

using TrainDude.Shared.Values;

public sealed record class StationLocationSet(Guid Id, Location Location) : IDomainEvent;