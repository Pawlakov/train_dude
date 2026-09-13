// <copyright file="IGeneralCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using TrainDude.Features.Shared.Contracts.Generic;

public interface IGeneralCommand
    : ICommand<EmptyResult>
{
}