// <copyright file="CreateSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.CreateSegment;

using System;

using TrainDude.Features.Base;
using TrainDude.Features.Segments.Domain.Values;

public sealed record CreateSegmentCommand(double NominalLength, int Tracks, SegmentEnd A, SegmentEnd B) : BaseCreateCommand;