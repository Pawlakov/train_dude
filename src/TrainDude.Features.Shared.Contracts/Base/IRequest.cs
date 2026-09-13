// <copyright file="IRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System.Text.Json.Serialization;

public interface IRequest<TResponse>
    where TResponse : IResponse
{
    [JsonIgnore]
    static abstract string Route { get; }
}