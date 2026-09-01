// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.Domain.Events;

using System;

using TrainDude.Features.Base;

public sealed record StationCreated(Guid Id, DateTime When, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : IHasAlternativeNames;