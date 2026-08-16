// <copyright file="UpdateStationNameModeCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Requests.Admin;

using TrainDude.Commands.Requests.Base;
using TrainDude.Integration.Values;

public sealed record class UpdateStationNameModeCommand
    : BasePolymorphicCommand
{
    required public StationNameMode Mode { get; set; }
}