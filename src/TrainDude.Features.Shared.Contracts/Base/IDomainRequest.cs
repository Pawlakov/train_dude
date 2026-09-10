// <copyright file="IDomainRequest.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>
namespace TrainDude.Features.Shared.Contracts.Base;

using System.Text.Json.Serialization;

public interface IDomainRequest
{
    [JsonIgnore]
    static abstract string Route { get; }
}