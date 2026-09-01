// <copyright file="LineTripAssignedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.ReadModels.Events;

using System;

using TrainDude.Features.Lines.Domain.Values;

public sealed record LineTripAssignedWithReferences(Guid Id, DateTime When, LineTrip Trip);