// <copyright file="CreateSegmentCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Segments.Contracts.CreateSegment;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateSegmentCommand(Guid SegmentId, double NominalLength, int Tracks, Guid AId, int AAxle, bool APole, Guid BId, int BAxle, bool BPole)
    : IGeneralCommand
{
    public const string TypeRoute = "/api/segments/create";

    public static string Route => TypeRoute;
}