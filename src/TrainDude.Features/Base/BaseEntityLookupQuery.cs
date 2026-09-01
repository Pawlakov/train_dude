// <copyright file="BaseEntityLookupQuery.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System;

public abstract record BaseEntityLookupQuery<TResult>(Guid Id) : BasePolymorphicQuery<TResult>;