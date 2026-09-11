// <copyright file="DropEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Features.Shared.Drop;

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Marten;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

using TrainDude.Features.Shared.Contracts.Admin;

using Wolverine.Http;

public static class DropEndpoint
{
    [WolverinePost(DropCommand.TypeRoute)]
    [Authorize(Policy = "SuperUser")]
    [Tags("Admin")]
    public static async Task<IResult> Handle(DropCommand command, ClaimsPrincipal user, IHost host, CancellationToken cancellationToken = default)
    {
        await host.ResetAllMartenDataAsync();

        var result = Results.Ok();

        return result;
    }
}