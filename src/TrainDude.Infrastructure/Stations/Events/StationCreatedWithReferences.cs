// <copyright file="StationCreatedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Stations.Events;

using System;

public sealed record StationCreatedWithReferences(Guid Id, DateTime When, string Name);