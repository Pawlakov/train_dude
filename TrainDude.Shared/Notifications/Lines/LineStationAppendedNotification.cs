// <copyright file="LineStationAppendedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Lines;

using System;

using Mediator;

public sealed record class LineStationAppendedNotification(Guid LineId, Guid StationId) : INotification;