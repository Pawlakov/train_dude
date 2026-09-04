// <copyright file="GetRadiiQueryResultItem.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Radii.Contracts.GetRadii;

using System;

public sealed record GetRadiiQueryResultItem(Guid RadiusId, int Speed, int Minimum, double MaximumAntiradius);