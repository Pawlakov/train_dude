// <copyright file="SetNameModeEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.SetNameMode;

using System;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Generic;
using TrainDude.Features.Shared;

using Wolverine;
using Wolverine.Http;

public static class SetNameModeEndpoint
{
    public const string Route = "/settings/name-mode/set";

    [WolverinePost(Route)]
    public static async Task<EmptyResponse> Handle(SetNameModeCommand command, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        await SettingsAccessor.ExecuteWithSettings(
        session,
        aggregate =>
        {
            aggregate.NamingPolicy = command.Mode;
            return Task.CompletedTask;
        },
        cancellationToken);

        var result = new EmptyResponse();

        return result;
    }
}