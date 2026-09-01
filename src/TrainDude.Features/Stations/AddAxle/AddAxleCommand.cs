// <copyright file="AddAxleCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Stations.AddAxle;

using System;

using TrainDude.Features.Base;

public sealed record AddAxleCommand(Guid StationId, long Version) : BaseUpdateCommand(StationId, Version);