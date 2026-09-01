// <copyright file="DropEndpoint.cs" company="Pawlakov">
// Copyright (c) Pawlakov. All rights reserved.
// </copyright>

namespace TrainDude.Infrastructure.Admin;

using System.Threading;
using System.Threading.Tasks;

using Marten;

using Wolverine;
using Wolverine.Http;

public static class DropEndpoint
{
    [WolverinePost(Route)]
    public static async Task<(EmptyResponse, OutgoingMessages)> Post(DropCommand command, IDocumentStore store, CancellationToken cancellationToken = default)
    {
        await store.Advanced.Clean.CompletelyRemoveAllAsync(cancellationToken);

        var response = new EmptyResponse();
        var integrationEvent = new DroppedIntegrationEvent();

        return (response, new OutgoingMessages { integrationEvent });
    }
}