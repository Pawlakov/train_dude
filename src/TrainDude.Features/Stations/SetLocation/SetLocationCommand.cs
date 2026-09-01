// <copyright file="SetLocationCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.SetLocation;

using System;

using TrainDude.Features.Base;
using TrainDude.Shared.Values;

public sealed record SetLocationCommand(Guid StationId, long Version, Location Location) : BaseUpdateCommand(StationId, Version);