// <copyright file="SegmentCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Segments;

using System;

using Mediator;

public class SegmentCreatedNotification(Guid SegmentId) : INotification;