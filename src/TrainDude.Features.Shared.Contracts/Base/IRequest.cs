// <copyright file="IRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Contracts.Base;

using System.Text.Json.Serialization;

public interface IRequest<TResult>
    where TResult : IResult
{
    [JsonIgnore]
    static abstract string Route { get; }
}