// <copyright file="StationCreated.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Stations.Domain.Events;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record StationCreated(Guid Id, DateTime When, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian) : IHasAlternativeNames;