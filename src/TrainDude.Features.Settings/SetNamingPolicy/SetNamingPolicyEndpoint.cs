// <copyright file="SetNamingPolicyEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.SetNamingPolicy;

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Settings.Contracts.SetNamingPolicy;
using TrainDude.Features.Shared.Contracts.Generic;
using TrainDude.Features.Shared.Extensions;

using Wolverine.Http;

public static class SetNamingPolicyEndpoint
{
    [WolverinePost(SetNamingPolicyCommand.TypeRoute)]
    [Tags("Settings")]
    public static async Task<EmptyResult> Handle(SetNamingPolicyCommand command, ClaimsPrincipal user, IDocumentSession session, CancellationToken cancellationToken = default)
    {
        await SettingsAccessor.ExecuteWithSettings(
        session,
        user,
        (stream, aggregate) =>
        {
            var set = aggregate.SetNamingPolicy(user.GetSubject(), command.Policy);
            stream.AppendOne(set);
            return Task.CompletedTask;
        },
        cancellationToken);

        var result = new EmptyResult();

        return result;
    }
}