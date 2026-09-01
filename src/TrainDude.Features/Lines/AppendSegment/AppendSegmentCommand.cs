// <copyright file="AppendSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.AppendSegment;

using System;

using TrainDude.Features.Base;

public sealed record AppendSegmentCommand(Guid LineId, long Version, Guid SegmentId) : BaseUpdateCommand(LineId, Version);