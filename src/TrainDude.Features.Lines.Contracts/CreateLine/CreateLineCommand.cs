// <copyright file="CreateLineCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.Contracts.CreateLine;

using TrainDude.Features.Shared.Contracts.Base;

public sealed record CreateLineCommand(int Number, char? Letter)
    : ICreateCommand
{
    public const string TypeRoute = "/line/create";

    public string Route => TypeRoute;
}