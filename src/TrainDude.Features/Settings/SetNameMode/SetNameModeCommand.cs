// <copyright file="SetNameModeCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.SetNameMode;

using TrainDude.Features.Base;
using TrainDude.Features.Generic;
using TrainDude.Shared.Enums;

public sealed record SetNameModeCommand(NamingPolicy Mode) : BaseEmptyCommand;