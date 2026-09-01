// <copyright file="CreateTripCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Trips.CreateTrip;

using TrainDude.Features.Base;

public sealed record CreateTripCommand(int Number) : BaseCreateCommand;