// <copyright file="CreateLineCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.CreateLine;

using System;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateLineCommand(Guid LineId, int Number, char? Letter)
    : IGeneralCommand
{
    public const string TypeRoute = "/api/lines/create";

    public static string Route => TypeRoute;
}