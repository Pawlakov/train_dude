// <copyright file="CreateRadiusCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.CreateRadius;

using System;

using TrainDude.Features.Base;

public sealed record CreateRadiusCommand(int Speed, int Minimum) : BaseCreateCommand;