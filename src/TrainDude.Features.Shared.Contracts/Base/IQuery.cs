// <copyright file="IQuery.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

public interface IQuery<TResponse>
    : IRequest<TResponse>
    where TResponse : IResponse
{
}