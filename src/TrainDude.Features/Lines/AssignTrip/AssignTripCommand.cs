// <copyright file="AssignTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AssignTrip;

using System;

using TrainDude.Features.Base;

public sealed record AssignTripCommand(Guid LineId, long Version, Guid TripId) : BaseUpdateCommand(LineId, Version);