// <copyright file="DropCommand.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Network.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

/// <summary>
/// A command which drops all network data.
/// </summary>
public class DropCommand : IRequest
{
}