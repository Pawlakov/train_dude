// <copyright file="GetNamingPolicyEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.GetNamingPolicy;

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Http;

using TrainDude.Features.Settings.Contracts.GetNamingPolicy;

using Wolverine.Http;

public static class GetNamingPolicyEndpoint
{
    [WolverineGet(GetNamingPolicyQuery.TypeRoute)]
    [Tags("Settings")]
    public static async Task<GetNamingPolicyResult> Handle([AsParameters] GetNamingPolicyQuery query, ClaimsPrincipal user, IQuerySession session, CancellationToken cancellationToken)
    {
        var queryResult = await SettingsAccessor.FetchForReading(session, user, cancellationToken);
        var result = new GetNamingPolicyResult(queryResult.NamingPolicy);

        return result;
    }
}