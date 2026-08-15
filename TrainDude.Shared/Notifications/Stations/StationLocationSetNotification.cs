// <copyright file="StationLocationSetNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Stations;

using System;

using Mediator;

using TrainDude.Shared.Values;

public sealed record class StationLocationSetNotification(Guid StationId, Location Location) : INotification;