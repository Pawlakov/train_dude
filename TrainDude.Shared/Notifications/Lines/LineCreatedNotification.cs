// <copyright file="LineCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Lines;

using System;

using Mediator;

public sealed record class LineCreatedNotification(Guid LineId, int LineNumber, char? LineLetter) : INotification;