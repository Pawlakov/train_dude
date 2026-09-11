// <copyright file="StationCreatedWithReferences.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Stations.Events;

using System;

using TrainDude.Features.Stations.Domain.Events;

public sealed record StationCreatedWithReferences(Guid StationId, string Who, string NameGerman, string? NameGermanNew, string? NamePolish, string? NameRussian, string Name);