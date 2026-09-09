// <copyright file="DropEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Drop;

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Authorization;

using TrainDude.Features.Shared.Contracts.Admin;
using TrainDude.Features.Shared.Contracts.Generic;

using Wolverine.Http;

public static class DropEndpoint
{
    [Authorize(Policy = "SuperUser")]
    [WolverinePost(DropCommand.TypeRoute)]
    public static async Task<EmptyResult> Post(DropCommand command, ClaimsPrincipal user, IDocumentStore store, CancellationToken cancellationToken = default)
    {
        await store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var response = new EmptyResult();

        return response;
    }
}