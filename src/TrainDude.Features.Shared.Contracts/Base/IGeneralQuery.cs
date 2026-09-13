// <copyright file="IGeneralQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IGeneralQuery<TResponse>
    : IQuery<TResponse>
    where TResponse : IResponse
{
}