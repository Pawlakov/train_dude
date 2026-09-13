// <copyright file="IListQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

public interface IListQuery<TResponse>
    : IGeneralQuery<TResponse>
    where TResponse : IResponse
{
}