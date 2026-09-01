// <copyright file="GetNameModeEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Settings.GetNameMode;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using TrainDude.Shared.Enums;

using Wolverine.Http;

public static class GetNameModeEndpoint
{
    public const string Route = "/settings/name-mode";

    [WolverineGet(Route)]
    public static async Task<GetNameModeQueryResult> Handle(GetNameModeQuery query, IDocumentSession session, CancellationToken cancellationToken)
    {
        var queryResult = await SettingsAccessor.FetchForReading(session, cancellationToken);
        var result = new GetNameModeQueryResult
        {
            Mode = queryResult.NamingPolicy,
        };

        return result;
    }
}