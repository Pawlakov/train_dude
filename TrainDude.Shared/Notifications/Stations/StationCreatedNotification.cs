// <copyright file="StationCreatedNotification.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Shared.Notifications.Stations;

using System;

using Mediator;

public sealed record class StationCreatedNotification(Guid StationId, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : INotification;