// <copyright file="BaseUpdateCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Base;

using System;

using TrainDude.Features.Generic;

public abstract record BaseUpdateCommand(Guid Id, long Version) : IVersionedDomainCommand<UpdatedResponse>;