// <copyright file="RadiusCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Radii;

using System;

using Mediator;

public sealed record class RadiusCreatedNotification(Guid RadiusId, int Speed, int Minimum) : INotification;