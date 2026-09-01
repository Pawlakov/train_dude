// <copyright file="BaseEntityListQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

public abstract record BaseEntityListQuery<TResult>
    : BasePolymorphicQuery<TResult>
{
}