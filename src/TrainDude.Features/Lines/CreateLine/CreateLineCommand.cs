// <copyright file="CreateLineCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Lines.CreateLine;

using TrainDude.Features.Base;

public sealed record CreateLineCommand(int Number, char? Letter) : BaseCreateCommand;