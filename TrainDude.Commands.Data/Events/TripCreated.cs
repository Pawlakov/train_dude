// <copyright file="TripCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Data.Events;

using System;

public record class TripCreated(Guid TripId, int TripNumber);