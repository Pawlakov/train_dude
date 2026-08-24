// <copyright file="StationAxleAdded.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Domain.Stations;

using System;

using TrainDude.Domain.Base;

public sealed record class StationAxleAdded(Guid Id, DateTime When) : BaseAggregateEvent(Id, When);