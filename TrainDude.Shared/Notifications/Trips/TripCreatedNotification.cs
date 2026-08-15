// <copyright file="TripCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Trips;

using System;

using Mediator;

public sealed record class TripCreatedNotification(Guid TripId, int TripNumber) : INotification;