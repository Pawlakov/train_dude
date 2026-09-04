// <copyright file="GetNamingPolicyEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.GetNamingPolicy;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Features.Settings.Contracts.GetNamingPolicy;

using Wolverine.Http;

public static class GetNamingPolicyEndpoint
{
    [WolverineGet(GetNamingPolicyQuery.TypeRoute)]
    public static async Task<GetNamingPolicyResult> Handle(GetNamingPolicyQuery query, IDocumentSession session, CancellationToken cancellationToken)
    {
        var queryResult = await SettingsAccessor.FetchForReading(session, cancellationToken);
        var result = new GetNamingPolicyResult(queryResult.NamingPolicy);

        return result;
    }
}