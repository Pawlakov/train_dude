// <copyright file="StationAxleAdded.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

public sealed record StationAxleAdded(Guid Id, DateTime When);