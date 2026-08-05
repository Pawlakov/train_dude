// <copyright file="DataChangedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Events;

using Mediator;

public sealed record class DataChangedNotification() : INotification;