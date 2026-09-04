// <copyright file="IDomainRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

using System.Text.Json.Serialization;

public interface IDomainRequest<TResult>
    where TResult : IRequestResult
{
    [JsonIgnore]
    string Route { get; }
}