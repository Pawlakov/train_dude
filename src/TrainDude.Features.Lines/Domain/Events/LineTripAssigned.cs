// <copyright file="LineTripAssigned.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Domain.Events;

using System;

public sealed record LineTripAssigned(Guid Id, string Who, Guid TripId);