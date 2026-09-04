// <copyright file="IDomainRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

public interface IDomainRequest<TResult>
    where TResult : IRequestResult
{
    string Route { get; }
}