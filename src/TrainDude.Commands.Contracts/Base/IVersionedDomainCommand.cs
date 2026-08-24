// <copyright file="IVersionedDomainCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Commands.Contracts.Base;

public interface IVersionedDomainCommand
    : IDomainCommand
{
    long Version { get; }
}